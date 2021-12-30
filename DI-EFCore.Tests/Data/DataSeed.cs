using System.Collections.Generic;

using DI_EFCore.Entities;
using DI_EFCore.Models;

namespace DI_EFCore.Tests.Data {

    public abstract class DataSeed {

        public static void Seed(AppDbContext context) {

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var user0 = new User("User0") {
                Posts = new List<Post>() {
                    new Post() {
                        PostContent = "Post0 Example",
                        Comments = new List<Comment>() {
                            new Comment() {CommentContent = "Comment in post0"}
                        }
                    }
                }
            };

            var user1 = new User("User1") {
                Posts = new List<Post>() {
                    new Post() { PostContent = "Post1" }
                }
            };

            context.Users.AddRange(user0, user1);

            context.SaveChanges();
        }
    }
}