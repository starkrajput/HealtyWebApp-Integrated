using Microsoft.EntityFrameworkCore;
using WebRazor_1.Models;

namespace WebRazor_1.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		// Everything with database happen here because it is dbcontext.
		// Now Categories Table will be created in db.[Entity Framweok Era]
		public DbSet<Category> Categories { get; set; }
		// add-migration AddCategorizTableToDb
		// then to certify these migrations -> update-database
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>().HasData(
				new Category { Id = 1, Name = "Action", DisplayCategory = 1 },
				new Category { Id = 2, Name = "Sci", DisplayCategory = 1 },
				new Category { Id = 3, Name = "His", DisplayCategory = 1 }
				);
		}
	}
}
