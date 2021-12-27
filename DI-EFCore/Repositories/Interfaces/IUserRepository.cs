using DI_EFCore.Entities;

namespace DI_EFCore.Repositories.Interfaces {

    public interface IUserRepository {

        /// <summary>Obtain all existing users</summary>
        /// <returns>An Enumerable with all Users</returns>
        Task<IEnumerable<User>> GetAllUsers();

        /// <summary>Get a user by its Id</summary>
        /// <param name="id">The User's Id</param>
        /// <returns>A User that contains the given Id or null if a user was not found</returns>
        Task<User?> GetUser(int id);

        /// <summary>Create a User</summary>
        /// <param name="user">The User to be created</param>
        /// <returns>The Created User</returns>
        Task<User> AddUser(User user);

        /// <summary>Delete a User</summary>
        /// <param name="user">The User to be Deleted</param>
        Task DeleteUser(User user);
    }
}