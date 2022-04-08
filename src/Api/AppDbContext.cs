
using Microsoft.EntityFrameworkCore;
using SaveAnAnimal.Api.Models;

namespace SaveAnAnimal.Api;

public class AppDbContext : DbContext
{
	public DbSet<Volunteer> Volunteers => Set<Volunteer>();
	public DbSet<Pet> Pets => Set<Pet>();
	public DbSet<PetCare> PetCares => Set<PetCare>();

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
