using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VCWalks.DataCollection
{
    public class VCWalksAuthDbContext : IdentityDbContext
    {
        public VCWalksAuthDbContext(DbContextOptions<VCWalksAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "4ece5b46 - 4724 - 42d2 - a143 - e6e1dc0ed0d9";
            var writerRoleId = "ce72279d-d9fd-42d0-8058-0f66446ddcc3";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id= readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName ="Reader".ToUpper()
                },
                new IdentityRole
                {
                     Id= writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName ="Writer".ToUpper()
                }

            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
