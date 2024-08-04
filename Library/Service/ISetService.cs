using Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Service
{
    public interface ISetService
    {
        Task<IEnumerable<SetModel>> GetAllSets();
        Task<SetModel?> GetSetById(long id);
        Task AddSet(SetModel set);
        Task UpdateSet(SetModel set);
        Task DeleteSet(long id);
    }
}
