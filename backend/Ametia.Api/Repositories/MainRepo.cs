using Grad.Controllers;
using Grad.Models;
using Grad.Repo.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Grad.Repo
{

    public class MainRepo<T> : IRepoBase<T> where T : class
    {
        private AppDbContext _context;
        public MainRepo(AppDbContext context)
        {
            _context = context;
        }

        public async  Task<IEnumerable<T>> GetAsyncAll()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T?> GetAsyncByParameter(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;
            T? res= await _context.Set<T>()
                .FirstOrDefaultAsync(entity => EF.Property<string>(entity, "Email") == email);

            return res;
        }
        public async Task<T> CreateAsync(T Entity)
        {
            var res = await _context.Set<T>().AddAsync(Entity);
            await _context.SaveChangesAsync(); // ✔️ استخدم async version
            return res.Entity;
        }

        public async Task<T> UpdateAsync(T Entity)
        {
             _context.Set<T>().Update(Entity);
            await _context.SaveChangesAsync();
            return Entity;
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<bool> DeleteAsync(int Entity)
        {
            T? ob=await GetByIdAsync(Entity);
            if (ob == null)
            {
                return false;
            }
            _context.Set<T>().Remove(ob);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
