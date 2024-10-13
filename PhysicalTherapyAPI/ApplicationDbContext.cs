using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhysicalTherapyAPI.Models;

namespace PhysicalTherapyAPI
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var model in builder.Model.GetEntityTypes())
            {
                var isDeletedProperty = model.FindProperty("IsDeleted");
                if (isDeletedProperty != null && isDeletedProperty.ClrType == typeof(bool))
                {
                    isDeletedProperty.SetDefaultValue(false);
                }
            }
            builder.Entity<Exercise>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Category>().HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
