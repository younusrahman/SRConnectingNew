using SRConnecting.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace SRConnecting.Services
{
    public class ServerService
    {




        private string connectinginfo = @"Server=srfamily.Database.windows.net;Database=SRConnecting;User Id=younusrahman;Password=Sayanrahman2018;";

            
       public bool Email_Exsites(string email)
        {
            switch (If_Email_Exsites(email))
            {
                case "1": return true;            

                default:
                    return false;
            }
        }



        public bool AddUser(UserDetails value)
        {
              return InsertUser(value.Firstname, value.Lastname, value.Email, value.Password, value.Mobile, value.Image);

  
        }


        public UserDetails FindUser(string email, string password)
        {
            return GetCurrentUserinfo(email, password);


        }




        private  bool InsertUser(string Firstname, string Lasttname, string Email, string Password, string Mobile, string Image)
        {


            try
            {
                using (SqlConnection conn = new SqlConnection(connectinginfo))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO UserDetails values (@Firstname,@Lastname,@Password,@Mobile,@Email,@Image)", conn))
                    {
                        cmd.Parameters.AddWithValue("@Firstname", Firstname);
                        cmd.Parameters.AddWithValue("@Lastname", Lasttname);
                        cmd.Parameters.AddWithValue("@Password", Password);
                        cmd.Parameters.AddWithValue("@Mobile", Mobile);
                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.Parameters.AddWithValue("@Image", Image);
                        cmd.ExecuteNonQuery();


                        conn.Close();

                    }
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }



        }



        private UserDetails GetCurrentUserinfo(string Email, string Password)
        {


            try
            {
                using (SqlConnection conn = new SqlConnection(connectinginfo))
                {

                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SearchUser", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add(new SqlParameter("@Email", Email));
                        //cmd.Parameters.Add(new SqlParameter("@Password", Password));

                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.Parameters.AddWithValue("@Password", Password);




                        SqlDataReader da = cmd.ExecuteReader();

                        
                        while (da.Read())
                        {
                            return new UserDetails()
                            {
                               
                                Firstname = da.GetValue(1).ToString(),
                                Lastname = da.GetValue(2).ToString(),
                                Password = da.GetValue(3).ToString(),
                                Mobile = da.GetValue(4).ToString(),
                                Email = da.GetValue(5).ToString(),
                                Image = da.GetValue(6).ToString(),

                            };

                        }

                        da.Close();
                        cmd.ExecuteNonQuery();


                        conn.Close();

                    }
                }

                return null;
            }
            catch (Exception)
            {

                return null;
            }



        }

        private string If_Email_Exsites(string Email)
        {


            try
            {
                using (SqlConnection conn = new SqlConnection(connectinginfo))
                {

                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SearchUser", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add(new SqlParameter("@Email", Email));
                        //cmd.Parameters.Add(new SqlParameter("@Password", Password));

                        cmd.Parameters.AddWithValue("@Email", Email);





                        SqlDataReader da = cmd.ExecuteReader();


                        while (da.Read())
                        {
                            return da.GetValue(0).ToString();

                        }

                        da.Close();
                        cmd.ExecuteNonQuery();


                        conn.Close();

                    }
                }

                return null;
            }
            catch (Exception)
            {

                return null;
            }



        }
    }
}
