using SaveAnAnimal.Api.Models;

namespace SaveAnAnimal.Api.Repositories;

public interface IVolunteerRepository
{
	IQueryable<Volunteer> GetAll();
	Volunteer? Get(Guid volunteerId);
	Task<Volunteer?> GetAsync(Guid volunteerId);
	void Add(Volunteer volunteer);
	void Update(Volunteer volunteer);
	void Delete(Volunteer volunteer);
	bool Delete(Guid volunteerId);
	void Save();
	Task SaveAsync();
}

public class VolunteerRepository : IVolunteerRepository, IDisposable
{
	private readonly AppDbContext _dbContext;

	public VolunteerRepository(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public void Add(Volunteer volunteer)
	{
		_dbContext.Volunteers.Add(volunteer);
	}

	public void Delete(Volunteer volunteer)
	{
		_dbContext.Volunteers.Remove(volunteer);
	}

	public bool Delete(Guid volunteerId)
	{
		var volunteer = Get(volunteerId);
		if (volunteer is null)
		{
			return false;
		}
		_dbContext.Volunteers.Remove(volunteer);
		return true;
	}

	public Volunteer? Get(Guid volunteerId)
	{
		return _dbContext.Volunteers.Find(volunteerId);
	}

	public IQueryable<Volunteer> GetAll()
	{
		return _dbContext.Volunteers.AsQueryable<Volunteer>();
	}

	public async Task<Volunteer?> GetAsync(Guid volunteerId)
	{
		return await _dbContext.Volunteers.FindAsync(volunteerId);
	}

	public void Save()
	{
		_dbContext.SaveChanges();
	}

	public async Task SaveAsync()
	{
		await _dbContext.SaveChangesAsync();
	}

	public void Update(Volunteer petCare)
	{
		_dbContext.Volunteers.Update(petCare);
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
