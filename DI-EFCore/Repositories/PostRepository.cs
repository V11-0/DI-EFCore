using DI_EFCore.Entities;
using DI_EFCore.Interfaces.Repositories;
using DI_EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace DI_EFCore.Repositories {

    public class PostRepository : IPostRepository {

        private readonly AppDbContext _context;

        public PostRepository(AppDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAllPosts() {
            return await _context.Posts.ToListAsync();
        }

        public async Task<Post?> GetPost(int id) {
            return await _context.Posts.FindAsync(id);
        }

        public async Task<Post> AddPost(Post post) {
            await _context.AddAsync(post);
            await _context.SaveChangesAsync();

            return post;
        }

        public async Task UpdatePost(int id, Post post) {
            post.Id = id;

            var entry = _context.Entry(post);
            entry.State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task DeletePost(int id) {
            var post = await GetPost(id);

            if (post != null) {
                _context.Remove(post);
                await _context.SaveChangesAsync();
            }
        }
    }
}