using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Usuario
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string City { get; set; }
        public DateTime NascDate { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int MyProperty { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public UnicodeEncoding UserActivation { get; set; }
    }
}
