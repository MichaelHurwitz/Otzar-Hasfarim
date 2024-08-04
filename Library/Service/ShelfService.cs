using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Service
{
    public class ShelfService : IShelfService
    {
        private readonly ApplicationDbContext _context;

        public ShelfService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShelfModel?>> GetAllShelves() =>
            await _context.Shelves
            .Include(s => s.Library)
            .ToListAsync();
        

        public async Task<ShelfModel?> GetShelfById(long id) =>
            await _context.Shelves
            .Include(s => s.Library)
            .FirstOrDefaultAsync(s => s.Id == id);
        

        public async Task AddShelf(ShelfModel shelf)
        {
            _context.Shelves
                .Add(shelf);
            await _context
                .SaveChangesAsync();
        }

        public async Task UpdateShelf(ShelfModel shelf)
        {
            _context.Shelves.Update(shelf);
            await _context
                .SaveChangesAsync();
        }

        public async Task DeleteShelf(long id)
        {
            var shelf = await _context.Shelves
                .FindAsync(id);
            if (shelf != null)
            {
                _context.Shelves
                    .Remove(shelf);
                await _context
                    .SaveChangesAsync();
            }
        }
    }
}
