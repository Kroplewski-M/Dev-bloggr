using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Dev_bloggr.Models;
using Microsoft.Identity.Client;
using Dev_bloggr.Models.BusinessModels;

namespace Dev_bloggr.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment>Comments { get; set; }
        public DbSet<BlogComments>BlogComments { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //CONFIGURE  BLOGS TABLE
            builder.Entity<Blog>().HasOne(b => b.User).WithMany().HasForeignKey(b => b.UserId);
            builder.Entity<Blog>().HasMany(b => b.BlogComments).WithOne(bc => bc.Blog).HasForeignKey(bc => bc.BlogId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comment>().HasOne(b => b.User).WithMany().HasForeignKey(b => b.UserId);
            builder.Entity<Comment>().HasMany(c => c.BlogComments).WithOne(bc => bc.Comment)
                .HasForeignKey(bc => bc.CommentId).OnDelete(DeleteBehavior.Restrict);

            //CONFIGURE IDENTITY TABLES
            //builder.HasDefaultSchema("Identity");
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable(name: "UserRoles");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable(name: "UserClaims");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable(name: "UserLogins");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable(name: "RoleClaims");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
        }
    }

}
