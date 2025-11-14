using BeeBuzz.Data.Interfaces;
using BeeBuzz.Data;
using Microsoft.EntityFrameworkCore;

namespace BeeBuzz.Data.Repositories
{
    public class BeeBuzzGenericGenericRepository<T> : IBeeBuzzGenericRepository<T> where T : class
    {
        internal readonly ILogger<BeeBuzzGenericGenericRepository<T>> _logger;
        internal readonly ApplicationDbContext _context;
        internal readonly DbSet<T> _dbSet;

        public BeeBuzzGenericGenericRepository(ApplicationDbContext db, ILogger<BeeBuzzGenericGenericRepository<T>> logger)
        {
            _logger = logger;
            _context = db;
            _dbSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
            try
            {
                _logger.LogInformation($"Add was called for {typeof(T).Name}");
                _dbSet.Add(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to add {typeof(T).Name}: {ex}");
                throw;
            }
        }

        public void Delete(T entity)
        {
            try
            {
                _logger.LogInformation($"Delete was called for {typeof(T).Name}");
                _dbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete {typeof(T).Name}: {ex}");
                throw;
            }
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                _logger.LogInformation("GetAll was called...");

                return _dbSet.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get: {ex}");
                return null;
            }
        }

        public T GetById(object id)
        {
            try
            {
                _logger.LogInformation($"GetById was called for {typeof(T).Name} with id: {id}");
                return _dbSet.Find(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get {typeof(T).Name} by id: {ex}");
                return null;
            }
        }

        public void SaveAll()
        {
            try
            {
                _logger.LogInformation("SaveAll was called");
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save changes: {ex}");
                throw;
            }
        }

        public void Update(T entity)
        {
            try
            {
                _logger.LogInformation($"Update was called for {typeof(T).Name}");
                _dbSet.Update(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to update {typeof(T).Name}: {ex}");
                throw;
            }
        }
    }
}
