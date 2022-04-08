using SaveAnAnimal.Api.Models;

namespace SaveAnAnimal.Api.Repositories;

public interface IPetRepository : IDisposable
{
	IQueryable<Pet> GetAll();
	Pet? Get(Guid petId);
	Task<Pet?> GetAsync(Guid petId);
	void Add(Pet pet);
	void Update(Pet pet);
	void Delete(Pet pet);
	void Delete(Guid petId);
	void Save();
	Task SaveAsync();
}

public class PetRepository : IPetRepository, IDisposable
{
	private readonly AppDbContext _dbContext;

	public PetRepository(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public IQueryable<Pet> GetAll()
	{
		return _dbContext.Pets.AsQueryable<Pet>();
	}

	public Pet? Get(Guid petId)
	{
		return _dbContext.Pets.Find(petId);
	}

	public async Task<Pet?> GetAsync(Guid petId)
	{
		return await _dbContext.Pets.FindAsync(petId);
	}

	public void Add(Pet pet)
	{
		_dbContext.Add(pet);
	}

	public void Update(Pet pet)
	{
		_dbContext.Update(pet);
	}

	public void Delete(Pet pet)
	{
		_dbContext.Remove(pet);
	}

	public void Delete(Guid petId)
	{
		var pet = Get(petId);
		if (pet is not null)
		{
			Delete(pet);
		}
	}

	public void Save()
	{
		_dbContext.SaveChanges();
	}

	public async Task SaveAsync()
	{
		await _dbContext.SaveChangesAsync();
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

