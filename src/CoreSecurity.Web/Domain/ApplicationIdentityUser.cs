using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace CoreSecurity.Web.Domain
{
    public class ApplicationIdentityUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
