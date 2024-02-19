using Microsoft.EntityFrameworkCore;
using RedisPoc.Entitites;
using RedisPoc.Persistence;
using RedisPoc.Repositories.Interfaces;

namespace RedisPoc.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task<User> Get(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<List<User>> Get()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task Create(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            _context.Remove(await _context.Users.FindAsync(id));
            await _context.SaveChangesAsync();
        }
    }
}