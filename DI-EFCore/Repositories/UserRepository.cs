using DI_EFCore.Entities;
using DI_EFCore.Interfaces.Repositories;
using DI_EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace DI_EFCore.Repositories {

    public class UserRepository : IUserRepository {

        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) {
            _context = context;
        }

        public async Task<List<User>> GetAllUsers() {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUser(int id) {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> AddUser(User user) {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task DeleteUser(User user) {
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}