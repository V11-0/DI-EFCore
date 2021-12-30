using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DI_EFCore.Entities;
using DI_EFCore.Repositories.Interfaces;

namespace DI_EFCore.Tests.Repositories.Mocks
{

    public class FakePostRepository : IPostRepository {

        List<Post> posts;

        public FakePostRepository() {
            posts = new List<Post>();

            Post post0 = new Post() {
                Id = 0,
                PostContent = "A post example text",
                AuthorId = 0,
                Comments = new List<Comment>()
            };

            Post post1 = new Post() {
                Id = 1,
                PostContent = "Another post example test",
                AuthorId = 1,
                Comments = new List<Comment>()
            };

            posts.Add(post0);
            posts.Add(post1);
        }

        public Task<Comment> AddComment(Comment comment) {
            int postId = comment.PostId;

            var post = posts.Where(p => p.Id == postId).SingleOrDefault();
            var validUsers = posts.Select(p => p.AuthorId).ToArray();

            // Simulate post or author not found
            if (post == null || (!validUsers.Contains(comment.AuthorId))) {
                throw new ArgumentException();
            }

            post.Comments!.Add(comment);
            return Task.FromResult(comment);
        }

        public Task<Post> AddPost(Post post) {

            // Valid User Ids for this fake repository
            if (post.AuthorId == 0 || post.AuthorId == 1) {
                return Task.FromResult(post);
            }

            // thrown when the user id does not exist
            throw new ArgumentException();
        }

        public Task DeletePost(Post post) {
            posts.Remove(post);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Post>> GetAllPosts() {
            return Task.FromResult(posts.AsEnumerable());
        }

        public Task<Post?> GetPost(int id) {
            var post = posts.Where(p => p.Id == id).SingleOrDefault();
            return Task.FromResult(post);
        }

        public Task UpdatePost(Post post) {
            return Task.CompletedTask;
        }
    }
}