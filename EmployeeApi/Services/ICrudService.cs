
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeApi.Services
{
    public interface ICrudService<T, PKey> : IService {
        Task<T> Create(T entity);

        Task<bool> Delete(T entity);

        Task<bool> Update(T entity);

        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(PKey id);
    }
}