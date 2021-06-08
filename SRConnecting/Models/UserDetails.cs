using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SRConnecting.Models
{
    public class UserDetails
    {
        [PrimaryKey, AutoIncrement]
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string Image { get; set; }
    }
}
