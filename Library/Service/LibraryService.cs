using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Service
{
    public class LibraryService : ILibraryService
    {
        private readonly ApplicationDbContext _context;

        public LibraryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LibraryModel>> GetAllLibraries() =>
        await _context.Library
            .Include(l => l.Shelves)
            .ToListAsync();
        

        public async Task<LibraryModel?> GetLibraryById(long id) =>
        await _context.Library
            .Include(l => l.Shelves)
            .FirstOrDefaultAsync(l => l.Id == id);
        

        public async Task AddLibrary(LibraryModel library)
        {
            _context.Library
                .Add(library);
            await _context
                .SaveChangesAsync();
        }

        public async Task UpdateLibrary(LibraryModel library)
        {
            _context.Library
                .Update(library);
               
            await _context
                .SaveChangesAsync();
        }

        public async Task DeleteLibrary(long id)
        {
            var library = await _context.Library
                .FindAsync(id);
            if (library != null)
            {
                _context.Library
                    .Remove(library);
                await _context
                    .SaveChangesAsync();
            }
        }
    }
}
