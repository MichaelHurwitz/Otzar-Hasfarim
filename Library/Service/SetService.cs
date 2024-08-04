using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Service
{
    public class SetService : ISetService
    {
        private readonly ApplicationDbContext _context;

        public SetService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SetModel>> GetAllSets() =>
        await _context.Sets
            .Include(s => s.Shelf)
            .ToListAsync();
        

        public async Task<SetModel?> GetSetById(long id) =>
        await _context.Sets
            .Include(s => s.Shelf)
            .FirstOrDefaultAsync(s => s.Id == id);

        public async Task AddSet(SetModel set)
        {
            _context.Sets
                .Add(set);
            await _context
                .SaveChangesAsync();
        }

        public async Task UpdateSet(SetModel set)
        {
            _context.Sets.Update(set);
            await _context
                .SaveChangesAsync();
        }

        public async Task DeleteSet(long id)
        {
            var set = await _context.Sets
                .FindAsync(id);
            if (set != null)
            {
                _context.Sets
                    .Remove(set);
                await _context
                    .SaveChangesAsync();
            }
        }
    }
}
