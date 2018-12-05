using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zv01.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser() : base()
        {
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }      
    }
}
