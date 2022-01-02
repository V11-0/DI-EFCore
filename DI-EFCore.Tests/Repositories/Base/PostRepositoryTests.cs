using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DI_EFCore.Entities;
using DI_EFCore.Models;
using DI_EFCore.Repositories;

using DI_EFCore.Tests.Data;

namespace DI_EFCore.Tests.Repositories.Base
{

    public abstract class PostRepositoryTests {

        public readonly DbContextOptions<AppDbContext> _contextOptions;

        protected PostRepositoryTests(DbContextOptions<AppDbContext> contextOptions) {
            _contextOptions = contextOptions;

            var context = new AppDbContext(contextOptions);
            DataSeed.Seed(context);
        }

        private PostRepository _repository() {
            return new PostRepository(new AppDbContext(_contextOptions));
        }

        [TestMethod]
        public async Task GetAllPosts_ReturnsPostEnumerable() {

            var posts = await _repository().GetAllPosts();
            var postCount = posts.Count();

            Assert.IsTrue(postCount > 0);
        }

        [TestMethod]
        [DataRow(1)]
        public async Task GetPost_ValidId_ReturnsPost(int postId) {
            var post = await _repository().GetPost(postId);
            Assert.IsInstanceOfType(post, typeof(Post));
        }

        [TestMethod]
        [DataRow(10)]
        public async Task GetPost_InvalidIdReturnsNull(int postId) {
            var post = await _repository().GetPost(postId);
            Assert.IsNull(post);
        }

        [TestMethod]
        public async Task AddPost_ValidPost_ReturnsPost() {
            var validPost = new Post() {
                AuthorId = 1,
                PostContent = "This is a valid post"
            };

            var addedPost = await _repository().AddPost(validPost);
            var newPostId = addedPost.Id;

            Assert.IsNotNull(newPostId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task AddPost_InvalidPost_ThrowsArgumentException() {

            var invalidPost = new Post() {
                AuthorId = 10, // Author does not exist
                PostContent = "This is an invalid Post"
            };

            var notAddedPost = await _repository().AddPost(invalidPost);
        }

        [TestMethod]
        [DataRow(1, 1, "Updated!")]
        public async Task UpdatePost_ValidId_CheckUpdatedPost(int postId, int authorId, string newPostContent) {

            var toUpdatePost = new Post() {
                PostContent = newPostContent,
                AuthorId = authorId,
                Id = postId
            };

            var repo = _repository();

            await repo.UpdatePost(toUpdatePost);

            var updatedPost = await repo.GetPost(postId);
            Assert.IsNotNull(updatedPost);

            var actualPostContent = updatedPost.PostContent;
            Assert.AreEqual(newPostContent, actualPostContent);
        }

        [TestMethod]
        [DataRow(10, "UpdatedContent")]
        [ExpectedException(typeof(KeyNotFoundException))]
        public async Task UpdatePost_InvalidId_ThrowsKeyNotFound(int postId, string content) {

            var invalidIdPost = new Post() {
                Id = postId,
                AuthorId = 0,
                PostContent = content
            };

            await _repository().UpdatePost(invalidIdPost);
        }

        [TestMethod]
        [DataRow(1, 10, "UpdatedContent")]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task UpdatePost_InvalidAuthorIdChange_ThrowsInvalidOperation(int postId, int authorId, string content) {

            var invalidPost = new Post() {
                Id = postId,
                AuthorId = authorId,
                PostContent = content
            };

            await _repository().UpdatePost(invalidPost);
        }

        [TestMethod]
        [DataRow(1)]
        public async Task DeletePost_ReturnsNothing(int postId) {
            var repo = _repository();
            var toDelete = await repo.GetPost(postId);

            Assert.IsNotNull(toDelete);

            await repo.DeletePost(toDelete);
        }

        [TestMethod]
        public async Task AddComment_ValidComment_ReturnsComment() {

            var validComment = new Comment() {
                CommentContent = "A Comment",
                AuthorId = 1,
                PostId = 1
            };

            var dbComment = await _repository().AddComment(validComment);
            Assert.IsInstanceOfType(dbComment, typeof(Comment));

            var commentId = dbComment.Id;
            Assert.IsNotNull(commentId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task AddComment_InvalidIds_ThrowsArgument() {

            var invalidComment = new Comment() {
                CommentContent = "A Comment",
                AuthorId = 10,
                PostId = 10
            };

            var dbComment = await _repository().AddComment(invalidComment);
        }
    }
}