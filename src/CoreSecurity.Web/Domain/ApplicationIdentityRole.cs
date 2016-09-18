using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CoreSecurity.Web.Domain
{
    public class ApplicationIdentityRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
