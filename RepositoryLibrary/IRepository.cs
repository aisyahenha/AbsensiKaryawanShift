using System.Linq.Expressions;

namespace RepositoryLibrary
{
    public interface IRepository<TModel>
    {
        Task<TModel> Save(TModel entity);
        Task<TModel>? FindById(int id, string[] includes = null);

        Task<TModel>? FindBy(Expression<Func<TModel, bool>> predicate, string[] includes = null);

        Task<IEnumerable<TModel>>? FindByGroup(Expression<Func<TModel, bool>> predicate, string[] includes = null);

        Task<bool> CheckIfExist(Expression<Func<TModel, bool>> predicate);
        Task<IEnumerable<TModel>> FindAll(string[] includes = null);

        Task<TModel>? Update(TModel entity);
        Task Delete(TModel entity);
        void detach(TModel entity);

    }
}
