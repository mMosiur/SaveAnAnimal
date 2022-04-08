using Microsoft.EntityFrameworkCore;
using SaveAnAnimal.Models;

public class AppDbContext : DbContext
{
	public DbSet<Volunteer> Volunteers => Set<Volunteer>();
	public DbSet<Pet> Pets => Set<Pet>();

	public AppDbContext(DbContextOptions options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder
			.Entity<Pet>()
			.Property(p => p.Type)
			.HasConversion(
				v => v.ToString(),
				v => (PetType)Enum.Parse(typeof(PetType), v)
			);
	}
}