using API_Active_Directory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Active_Directory.Interfaces
{
    public interface IAuthenticationService
    {
        User Login(string userName, string password);
    }
}
