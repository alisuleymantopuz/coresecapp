using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreSecurity.Web.Domain
{
    public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationIdentityUser, ApplicationIdentityRole, string>
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
        : base(options)
        {

        }
    }
}
