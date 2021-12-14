using Microsoft.EntityFrameworkCore;

using DI_EFCore.Entities;

namespace DI_EFCore.Models {
    public class AppDbContext : DbContext {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Post> Posts => Set<Post>();
    }
}
