using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeApi.Repository;
using Microsoft.Extensions.Logging;

namespace EmployeeApi.Services
{
    public abstract class CrudService<T, PKey> : ICrudService<T, PKey> where T : Entity<PKey>
    {
        protected readonly ILogger<CrudService<T, PKey>> logger;
        protected readonly ICrudRepository<T, PKey> repository;
        private readonly string entityName;
        public CrudService(
            ILogger<CrudService<T, PKey>> logger,
            ICrudRepository<T, PKey> crudRepository) : this(logger, crudRepository, typeof(T).Name) {
        }
        public CrudService(
            ILogger<CrudService<T, PKey>> logger,
            ICrudRepository<T, PKey> crudRepository,
            string entityName) {
            this.logger = logger;
            this.repository = crudRepository;
            this.entityName = entityName;
        }
        public virtual Task<T> Create(T entity)
        {
            logger.LogInformation($"Create {entityName} with id {entity.Id}");
            return repository.Insert(entity);
        }

        public virtual Task<bool> Delete(T entity)
        {
            logger.LogInformation($"Delete {entityName} with id {entity.Id}");
            return repository.Delete(entity);
        }

        public virtual Task<IEnumerable<T>> GetAll()
        {
            logger.LogInformation($"Get all {entityName}");
            return repository.GetAll();
        }

        public virtual Task<T> GetById(PKey id)
        {
            logger.LogInformation($"get {entityName} by id: {id}");
            return repository.GetById(id);
        }

        public Task<bool> Update(T entity)
        {
            logger.LogInformation($"update {entityName} by id: {entity.Id}");
            return repository.Update(entity);
        }
    }
}