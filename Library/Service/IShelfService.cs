using Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Service
{
    public interface IShelfService
    {
        Task<IEnumerable<ShelfModel?>> GetAllShelves();
        Task<ShelfModel?> GetShelfById(long id);
        Task AddShelf(ShelfModel shelf);
        Task UpdateShelf(ShelfModel shelf);
        Task DeleteShelf(long id);
    }
}
