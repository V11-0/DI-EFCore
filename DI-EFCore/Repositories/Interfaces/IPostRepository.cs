using DI_EFCore.Entities;

namespace DI_EFCore.Repositories.Interfaces {

    public interface IPostRepository {

        /// <summary>Obtain All Existing Posts from Data Source</summary>
        /// <returns>An Enumerable with all Posts</returns>
        Task<IEnumerable<Post>> GetAllPosts();

        /// <summary>Obtain the Post with it's given Id</summary>
        /// <param name="id">The Id of the Post</param>
        /// <returns>The corresponding Post or null if the Post was not found</returns>
        Task<Post?> GetPost(int id);

        /// <summary>Create a Post in the data source</summary>
        /// <param name="post">The Post to be created</param>
        /// <returns>The Created Post</returns>
        /// <exception cref="ArgumentException">If the Post Author was not found</exception>
        Task<Post> AddPost(Post post);

        /// <summary>Update the information of an existing post</summary>
        /// <param name="post">The Post to be updated</param>
        Task UpdatePost(Post post);

        /// <summary>Delete a Post</summary>
        /// <param name="post">The Post to be Deleted</param>
        Task DeletePost(Post post);

        /// <summary>Create a Comment in a Post</summary>
        /// <param name="comment">The Comment to be added to it's Post</param>
        /// <exception cref="ArgumentException">If the Author or Post specified were not found</exception>
        /// <returns>The Created Comment</returns>
        Task<Comment> AddComment(Comment comment);
    }
}