using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class ReservationDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        private const string SQL_GetNext30DaysReservations = @"select * from reservation join site on reservation.site_id = site.site_id join campground on site.campground_id = campground.campground_id where park_id = @park_id and from_date >= @currentDay and from_date <= @30Dayfuture ORDER BY from_date;";
        private const string SQL_GetOpenings = @"select * from site join campground on site.campground_id= campground.campground_id where site.campground_id=@campground_id and site.site_id not in (select Distinct site.site_id from site join reservation on site.site_id = reservation.site_id where site.campground_id = @campground_id and ((from_date <= @to_date and from_date >= @from_date) or (to_date <= @to_date and to_date >= @from_date) or (@from_date >= from_date and @from_date <= to_date)));";
        private const string SQL_GetOpeningsAll = @"select * from site join campground on site.campground_id= campground.campground_id join park on campground.park_id = park.park_id where park.park_id = @park_id and site.site_id not in (select Distinct site.site_id from site join reservation on site.site_id = reservation.site_id where park.park_id = @park_id and ((from_date <= @to_date and from_date >= @from_date) or (to_date <= @to_date and to_date >= @from_date) or (@from_date >= from_date and @from_date <= to_date))) ORDER BY campground.daily_fee;";
        private const string SQL_InsertReservation = @"insert into reservation values(@site_id, @name, @from_date, @to_date, @create_date);";
        private const string SQL_ReturnReservationId = @"select reservation_id from reservation order by reservation_id desc;";
        
        public List<Site> GetOpenings(int park_id, int campground_id, DateTime from_date, DateTime to_date)
        {

            List<Site> output = new List<Site>();

            try
            {
                
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd;
                    if (campground_id == 0)
                    {
                        cmd = new SqlCommand(SQL_GetOpeningsAll, connection);
                        cmd.Parameters.AddWithValue("@park_id", park_id);
                        cmd.Parameters.AddWithValue("@from_date", from_date.Date);
                        cmd.Parameters.AddWithValue("@to_date", to_date.Date);
                    }
                    else
                    {
                        cmd = new SqlCommand(SQL_GetOpenings, connection);
                        cmd.Parameters.AddWithValue("@campground_id", campground_id);
                        cmd.Parameters.AddWithValue("@from_date", from_date.Date);
                        cmd.Parameters.AddWithValue("@to_date", to_date.Date);
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        Site s = new Site();

                        s.Campground_Id = Convert.ToInt32(reader["campground_id"]);
                        s.Site_Id = Convert.ToInt32(reader["site_id"]);
                        s.Max_Occupancy = Convert.ToInt32(reader["max_occupancy"]);
                        s.Max_Rv_Length = Convert.ToInt32(reader["max_rv_length"]);
                        s.Site_Number = Convert.ToInt32(reader["site_number"]);
                        int utilities = Convert.ToInt32(reader["utilities"]);
                        if(utilities==1)
                        {
                            s.HasUtilities = true;
                        }
                        int accessible = Convert.ToInt32(reader["accessible"]);
                        if(accessible==1)
                        {
                            s.WheelchairAccessible = true;
                        }

                        output.Add(s);
                    }

                }
            }
            catch(Exception)
            {
                throw;
            }

            return output;
        }

        public List<Reservation> GetNext30DaysReservations(int park_Id)
        {

            List<Reservation> output = new List<Reservation>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetNext30DaysReservations, connection);
                    cmd.Parameters.AddWithValue("@park_id", park_Id);
                    cmd.Parameters.AddWithValue("@currentDay", DateTime.Today.Date);
                    cmd.Parameters.AddWithValue("@30DayFuture", DateTime.Today.Date.AddDays(30));

                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        Reservation r = new Reservation();

                        r.Reservation_Id = Convert.ToInt32(reader["reservation_id"]);
                        r.Site_Id = Convert.ToInt32(reader["site_id"]);
                        r.Name = Convert.ToString(reader["name"]);
                        r.From_Date = Convert.ToDateTime(reader["from_date"]);
                        r.To_Date = Convert.ToDateTime(reader["to_date"]);

                        output.Add(r);
                    }

                }
            }
            catch(Exception)
            {
                throw;
            }

            return output;
        }

        public int InsertReservation(Reservation newReservation)
        {
            try
            {
                int reservation_id = 0;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_InsertReservation, connection);
                    cmd.Parameters.AddWithValue("@site_id", newReservation.Site_Id);
                    cmd.Parameters.AddWithValue("@name", newReservation.Name);
                    cmd.Parameters.AddWithValue("@from_date", newReservation.From_Date);
                    cmd.Parameters.AddWithValue("@to_date", newReservation.To_Date);
                    cmd.Parameters.AddWithValue("@create_date", newReservation.Reservation_DateTime);

                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand(SQL_ReturnReservationId, connection);
                    reservation_id = Convert.ToInt32(cmd.ExecuteScalar());
                }

                return reservation_id;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
