using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;


namespace RepositoryLibrary
{
    public class Repository<TModel> : IRepository<TModel> where TModel : class
    {
        private readonly DbContext _db;

        public Repository(DbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<bool> CheckIfExist(Expression<Func<TModel, bool>> predicate)
        {
            return await _db.Set<TModel>().AnyAsync(predicate);
        }

        public async Task Delete(TModel model)
        {
            _db.Set<TModel>().Attach(model);
            _db.Set<TModel>().Remove(model);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<TModel>> FindAll(string[] includes = null)
        {
            if (includes is null)
                return await _db.Set<TModel>().ToListAsync();

            IQueryable<TModel> db = _db.Set<TModel>();
            foreach (var include in includes)
            {
                db = db.Include(include);
            }
            return await db.ToListAsync();
        }

        public async Task<TModel> FindBy(Expression<Func<TModel, bool>> predicate, string[] includes = null)
        {
            if (includes is null)
                return await _db.Set<TModel>().FirstOrDefaultAsync(predicate);

            IQueryable<TModel> db = _db.Set<TModel>();
            foreach (var include in includes)
            {
                db = db.Include(include);
            }
            return await db.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TModel>> FindByGroup(Expression<Func<TModel, bool>> predicate, string[] includes = null)
        {
            if (includes is null)
                return await _db.Set<TModel>().Where(predicate).ToListAsync();

            IQueryable<TModel> db = _db.Set<TModel>();
            foreach (var include in includes)
            {
                db = db.Include(include);
            }
            return await db.Where(predicate).ToListAsync();
        }

        public async Task<TModel> FindById(int id, string[] includes = null)
        {
            if (includes is null)
                return await _db.Set<TModel>().FindAsync(id);

            IQueryable<TModel> db = _db.Set<TModel>();
            foreach (var include in includes)
            {
                db = db.Include(include);
            }
            return await db.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task<TModel> Save(TModel model)
        {
            try
            {
                var newModel = await _db.Set<TModel>().AddAsync(model);
                await _db.SaveChangesAsync();
                return newModel.Entity;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }

        }

        public async Task<TModel> Update(TModel model)
        {
            _db.Set<TModel>().Update(model);
            await _db.SaveChangesAsync();
            return model;
        }
        public void detach(TModel entity)
        {
            _db.Entry(entity).State = EntityState.Detached;
        }

    }
}
