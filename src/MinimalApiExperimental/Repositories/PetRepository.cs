using SaveAnAnimal.Models;

namespace SaveAnAnimal.Repositories;

public class PetRepository : IDisposable
{
	private AppDbContext _dbContext;

	public PetRepository(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public IQueryable<Pet> GetPets()
	{
		return _dbContext.Pets;
	}

	public Pet? GetPet(Guid id)
	{
		return _dbContext.Pets.Find(id);
	}

	public async Task<Pet?> GetPetAsync(Guid id)
	{
		return await _dbContext.Pets.FindAsync(id);
	}

	public void InsertPet(Pet pet)
	{
		_dbContext.Pets.Add(pet);
		_dbContext.SaveChanges();
	}

	public async Task InsertPetAsync(Pet pet)
	{
		_dbContext.Pets.Add(pet);
		await _dbContext.SaveChangesAsync();
	}

	public void DeletePet(Guid id)
	{
		var Pet = _dbContext.Pets.Find(id);
		if(Pet is not null)
		{
			DeletePet(Pet);
		}
	}

	public void DeletePet(Pet pet)
	{
		_dbContext.Pets.Remove(pet);
	}

	public void UpdatePet(Pet pet)
	{
		_dbContext.Pets.Update(pet);
	}

	public void Save(Pet pet)
	{
		_dbContext.Add(pet);
	}

	private bool _disposed = false;

	protected virtual void Dispose(bool disposing)
	{
		if (_disposed) return;
		if (disposing)
		{
			// Dispose managed state (managed objects)
			_dbContext.Dispose();
		}
		_disposed = true;
	}

	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
