using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;

using DI_EFCore.Controllers;
using DI_EFCore.Tests.Repositories;
using DI_EFCore.Entities;

namespace DI_EFCore.Tests.Controllers {

    [TestClass]
    public class PostControllerTests {

        private PostController _controller;

        public PostControllerTests() {
            _controller = new PostController(new FakePostRepository());
        }

        [TestMethod]
        public async Task GetAllPosts_ReturnsEnumerable() {
            var actionResult = (await _controller.GetAllPosts()).Result;

            var okResult = actionResult as OkObjectResult;
            Assert.IsNotNull(okResult);

            var actualValue = okResult.Value;
            Assert.IsInstanceOfType(actualValue, typeof(IEnumerable<Post>));
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        public async Task GetPost_ValidId_ReturnsPost(int postId) {
            var actionResult = (await _controller.GetPost(postId)).Result;

            var okResult = actionResult as OkObjectResult;
            Assert.IsNotNull(okResult);

            var post = okResult.Value;
            Assert.IsInstanceOfType(post, typeof(Post));
        }

        [TestMethod]
        [DataRow(10)]
        [DataRow(20)]
        public async Task GetPost_InvalidId_ReturnsNotFound(int postId) {
            var actionResult = (await _controller.GetPost(postId)).Result;
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task AddPost_ValidPost_ReturnsPost() {

            // Valid Post
            var post = new Post() {
                PostContent = "Post content example",
                AuthorId = 0
            };

            var actionResult = (await _controller.AddPost(post)).Result;

            Assert.IsInstanceOfType(actionResult, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task AddPost_InvalidPost_ReturnsBadRequestObject() {

            // Invlid Post, Author does not exist
            var post = new Post() {
                PostContent = "Post content example",
                AuthorId = 10
            };

            var actionResult =  (await _controller.AddPost(post)).Result;

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task UpdatePost_ValidPost_ReturnsNoContent() {

            // Valid Post
            var validPost = new Post() {
                Id = 0,
                PostContent = "Post content has changed"
            };

            var actionResult = await _controller.UpdatePost(validPost);

            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task UpdatePost_InvalidPost_ReturnsNotFound() {

            // Invalid Post Id
            var invalidPost = new Post() {
                Id = 10,
                PostContent = "Updated Post Content"
            };

            var actionResult = await _controller.UpdatePost(invalidPost);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        [DataRow(0)]
        public async Task DeletePost_ValidId_ReturnsNoContent(int postId) {

            var actionResult = await _controller.DeletePost(postId);

            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }

        [TestMethod]
        [DataRow(10)]
        public async Task DeletePost_InvalidId_ReturnsNotFound(int postId) {
            var actionResult = await _controller.DeletePost(postId);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task AddComment_InvalidUserId_ReturnsBadRequestObject() {
            var invalidComment = new Comment() {
                AuthorId = 10, // Nonexistent UserId
                CommentContent = "Comment Example",
                PostId = 1
            };

            var actionResult = (await _controller.PostComment(invalidComment)).Result;
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task AddComment_InvalidPostId_ReturnsBadRequestObject() {
            var invalidComment = new Comment() {
                AuthorId = 1,
                CommentContent = "Comment Example",
                PostId = 10 // Nonexistent postId
            };

            var actionResult = (await _controller.PostComment(invalidComment)).Result;
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task AddComment_InvalidPostAndUserId_ReturnsBadRequestObject() {
            var invalidComment = new Comment() {
                AuthorId = 10, // Nonexistent UserId
                CommentContent = "Comment Example",
                PostId = 10 // Nonexistent PostId
            };

            var actionResult = (await _controller.PostComment(invalidComment)).Result;
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task AddComment_ValidComment_ReturnsOkObject() {
            var invalidComment = new Comment() {
                AuthorId = 1,
                CommentContent = "Comment Example",
                PostId = 1
            };

            var actionResult = (await _controller.PostComment(invalidComment)).Result;
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        }
    }
}