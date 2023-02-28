using System.Collections;

namespace MovieCharactersAPI.Services
{
    public interface ICrudService<T, ID>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(ID id);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task DeleteById(ID id);
    }
}
