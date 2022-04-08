using SaveAnAnimal.Models;

namespace SaveAnAnimal.Repositories;

public class VolunteerRepository : IDisposable
{
	private AppDbContext _dbContext;

	public VolunteerRepository(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public IQueryable<Volunteer> GetVolunteers()
	{
		return _dbContext.Volunteers;
	}

	public Volunteer? GetVolunteer(Guid id)
	{
		return _dbContext.Volunteers.Find(id);
	}

	public async Task<Volunteer?> GetVolunteerAsync(Guid id)
	{
		return await _dbContext.Volunteers.FindAsync(id);
	}

	public void InsertVolunteer(Volunteer volunteer)
	{
		_dbContext.Volunteers.Add(volunteer);
		_dbContext.SaveChanges();
	}

	public async Task InsertVolunteerAsync(Volunteer volunteer)
	{
		_dbContext.Volunteers.Add(volunteer);
		await _dbContext.SaveChangesAsync();
	}

	public void DeleteVolunteer(Guid id)
	{
		var volunteer = _dbContext.Volunteers.Find(id);
		if(volunteer is not null)
		{
			DeleteVolunteer(volunteer);
		}
	}

	public void DeleteVolunteer(Volunteer volunteer)
	{
		_dbContext.Volunteers.Remove(volunteer);
	}

	public void UpdateVolunteer(Volunteer volunteer)
	{
		_dbContext.Volunteers.Update(volunteer);
	}

	public void Save(Volunteer volunteer)
	{
		_dbContext.Add(volunteer);
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
