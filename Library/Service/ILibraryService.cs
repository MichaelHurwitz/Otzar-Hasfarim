using Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Service
{
    public interface ILibraryService
    {
        Task<IEnumerable<LibraryModel>> GetAllLibraries();
        Task<LibraryModel?> GetLibraryById(long id);
        Task AddLibrary(LibraryModel library);
        Task UpdateLibrary(LibraryModel library);
        Task DeleteLibrary(long id);
    }
}
