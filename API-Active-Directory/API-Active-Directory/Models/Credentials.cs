using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Active_Directory.Models
{
    public class Credentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string LdapSection { get; set; }
    }
}
