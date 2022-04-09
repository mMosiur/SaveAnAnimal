using SaveAnAnimal.Api.Models;

namespace SaveAnAnimal.Api.Repositories;

public interface IPetCareRepository
{
	IQueryable<PetCare> GetAll();
	PetCare? Get(Guid petCareId);
	Task<PetCare?> GetAsync(Guid petCareId);
	void Add(PetCare petCare);
	void Update(PetCare petCare);
	void Delete(PetCare petCare);
	bool Delete(Guid petCareId);
	void Save();
	Task SaveAsync();
}

public class PetCareRepository : IPetCareRepository, IDisposable
{
	private readonly AppDbContext _dbContext;

	public PetCareRepository(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public void Add(PetCare petCare)
	{
		_dbContext.PetCares.Add(petCare);
	}

	public void Delete(PetCare petCare)
	{
		_dbContext.PetCares.Remove(petCare);
	}

	public bool Delete(Guid petCareId)
	{
		var petCare = Get(petCareId);
		if (petCare is null)
		{
			return false;
		}
		Delete(petCare);
		return true;
	}

	public IQueryable<PetCare> GetAll()
	{
		return _dbContext.PetCares.AsQueryable<PetCare>();
	}

	public PetCare? Get(Guid petCareId)
	{
		return _dbContext.PetCares.Find(petCareId);
	}

	public async Task<PetCare?> GetAsync(Guid petCareId)
	{
		return await _dbContext.PetCares.FindAsync(petCareId);
	}

	public void Save()
	{
		_dbContext.SaveChanges();
	}

	public async Task SaveAsync()
	{
		await _dbContext.SaveChangesAsync();
	}

	public void Update(PetCare petCare)
	{
		_dbContext.PetCares.Update(petCare);
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
