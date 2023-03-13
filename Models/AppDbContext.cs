using Microsoft.EntityFrameworkCore;

namespace test.Models
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
		public DbSet<Account>? Accounts{ get; set; }
		public DbSet<UserRole>? Roles { get; set; } 
	}
}