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

        public async Task<Post?> AddPost(Post post) {

            var user = await _context.Users.FindAsync(post.AuthorId);

            if (user != null) {

                if (user.Posts == null) {
                    user.Posts = new List<Post>();
                }

                user.Posts.Add(post);
                await _context.SaveChangesAsync();

                return post;
            }

            return null;
        }

        public async Task UpdatePost(Post post) {

            var entry = _context.Entry(post);
            entry.State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task DeletePost(Post post) {
            _context.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task<Comment?> AddComment(Comment comment) {
            var post = await _context.Posts.FindAsync(comment.PostId);
            var user = await _context.Users.FindAsync(comment.AuthorId);

            if (post != null && user != null) {
                if (post.Comments == null) {
                    post.Comments = new List<Comment>();
                }

                post.Comments.Add(comment);
                await _context.SaveChangesAsync();

                return comment;
            }

            return null;
        }
    }
}