using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace thebourne.Models
{
    public partial class AspNetUsers
    {
        private string _password;
        public string FullName => this.FirstName + " " + this.LastName;
        public string GetUserRoles => string.Join(",", this.AspNetRoles.Select(x => x.Name));
        public string UserRoles { get; set; }

        public string Password
        {
            get
            {
                return _password;

            }
            set
            {
              
                _password = value;
            }
        }
    }
}