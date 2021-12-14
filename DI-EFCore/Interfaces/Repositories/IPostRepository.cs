using DI_EFCore.Entities;

namespace DI_EFCore.Interfaces.Repositories {

    public interface IPostRepository {
        Task<IEnumerable<Post>> GetAllPosts();
        Task<Post?> GetPost(int id);
        Task<Post> AddPost(Post post);
        Task UpdatePost(int id, Post post);
        Task DeletePost(int id);
    }
}