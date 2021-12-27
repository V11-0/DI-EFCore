using Microsoft.EntityFrameworkCore;

using DI_EFCore.Entities;
using DI_EFCore.Repositories.Interfaces;
using DI_EFCore.Models;

namespace DI_EFCore.Repositories {

    public class PostRepository : IPostRepository {

        private readonly AppDbContext _context;

        public PostRepository(AppDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAllPosts() {
            return await _context.Posts.ToArrayAsync();
        }

        public async Task<Post?> GetPost(int id) {
            return await _context.Posts.FindAsync(id);
        }

        public async Task<Post> AddPost(Post post) {

            var user = await _context.Users.FindAsync(post.AuthorId);

            if (user == null) {
                throw new ArgumentException("Invalid AuthorId, User does not exist");
            }

            if (user.Posts == null) {
                user.Posts = new List<Post>();
            }

            user.Posts.Add(post);
            await _context.SaveChangesAsync();

            return post;
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

        public async Task<Comment> AddComment(Comment comment) {

            var post = await _context.Posts.FindAsync(comment.PostId);
            var user = await _context.Users.FindAsync(comment.AuthorId);

            if (post == null || user == null) {
                throw new ArgumentException("Invalid PostId or AuthorId");
            }

            if (post.Comments == null) {
                post.Comments = new List<Comment>();
            }

            post.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return comment;
        }
    }
}