using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ViewModelTableFormation.Data
{
    public class SchoolIdentityDbContext:IdentityDbContext
    {
        public SchoolIdentityDbContext(DbContextOptions<SchoolIdentityDbContext> options):base(options) { }
    }
}
