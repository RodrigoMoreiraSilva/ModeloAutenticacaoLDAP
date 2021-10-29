using API_Active_Directory.Interfaces;
using API_Active_Directory.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Threading.Tasks;

namespace API_Active_Directory.Services
{
    public class LdapAuthenticationService : IAuthenticationService
    {
        private const string DisplayNameAttribute = "DisplayName";
        private const string SAMAccountNameAttribute = "SAMAccountName";

        private readonly LdapConfig config;

        public LdapAuthenticationService(IOptions<LdapConfig> config)
        {
            this.config = config.Value;
        }

        public User Login(string userName, string password)
        {
            try
            {
                using (DirectoryEntry entry = new DirectoryEntry(config.Path, config.UserDomainName + "\\" + userName, password))
                {
                    using (DirectorySearcher searcher = new DirectorySearcher(entry))
                    {
                        searcher.Filter = String.Format("({0}={1})", SAMAccountNameAttribute, userName);
                        //Se comentar os propertiesToLoad que filtram as propriedades retornadas, por padrão serão retornadas todas as 37 propriedades existentes no AD
                        searcher.PropertiesToLoad.Add(DisplayNameAttribute);
                        searcher.PropertiesToLoad.Add(SAMAccountNameAttribute);
                        
                        var result = searcher.FindOne();

                        if (result != null)
                        {
                            var displayName = result.Properties[DisplayNameAttribute];
                            var samAccountName = result.Properties[SAMAccountNameAttribute];

                            return new User
                            {
                                DisplayName = displayName == null || displayName.Count <= 0 ? null : displayName[0].ToString(),
                                UserName = samAccountName == null || samAccountName.Count <= 0 ? null : samAccountName[0].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ex;
                return null;
            }
            return null;
        }
    }
}
