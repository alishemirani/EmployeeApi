using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeApi.Repository
{
    public interface ICrudRepository<T, PKey> : IRepository where T : Entity<PKey> {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(PKey id);
        Task<bool> Update(T entity);
        Task<T> Insert(T entity);
        Task<bool> Delete(T entity);
    }
}