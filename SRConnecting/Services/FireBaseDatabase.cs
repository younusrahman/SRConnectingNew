using Firebase.Database;
using SRConnecting.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Firebase.Database.Query;

namespace SRConnecting.Services
{
    class FireBaseDatabase
    {
        FirebaseClient client;
        private string baseUrl = "https://userdatabase-7d2be-default-rtdb.firebaseio.com/";
        
        public FireBaseDatabase()
        {
            client = new FirebaseClient(baseUrl);
        }   

        public async Task<bool>IsUserExists(string email)
        {
            var user = (await client.Child("Users")
                .OnceAsync<UserDetails>()).Where(u => u.Object.Email == email);
            if(user.FirstOrDefault() == null)
            {
                return false;
            }
            else
            {
                return true;
            }
               
        }
        
        public async Task<bool>RegisterUser(string fname, 
                                            string lname,
                                            string email,
                                            string password,
                                            string mobile,
                                            string imageurl)
        {

            var a = IsUserExists(email);

            if (await IsUserExists(email) == false)
            {
                await client.Child("Users")
                    .PostAsync(new UserDetails()
                    {
                        Firstname = fname,
                        Lastname = lname,
                        Password = password,
                        Mobile = mobile,
                        Email = email,
                        Image = imageurl
                    });

                return true;
            }
            else
            { return false; }
        }

    public async Task<bool> LoginUser(string email, string pass)
    {
            var user = (await client.Child("Users")
                    .OnceAsync<UserDetails>()).Where(u => u.Object.Email == email && u.Object.Password == pass).FirstOrDefault();

            return (user != null);
    }


    }

}
