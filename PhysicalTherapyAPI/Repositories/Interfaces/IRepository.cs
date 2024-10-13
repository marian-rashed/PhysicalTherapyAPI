using System.Linq.Expressions;

namespace PhysicalTherapyAPI.Repositories.Inplementation
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includeProps = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProps = null);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        IEnumerable<T> GetList(Expression<Func<T, bool>> filter, string? includeProps = null);
    }
}
