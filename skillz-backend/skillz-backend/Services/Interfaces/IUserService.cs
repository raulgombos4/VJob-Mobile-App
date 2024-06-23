using System;
using System.Threading.Tasks;
using skillz_backend.data;
using skillz_backend.models;
using skillz_backend.Repositories;

namespace skillz_backend.Services
{
    // Interface defining operations related to the User entity in the service.
    public interface IUserService
    {
        // Retrieves a user by their unique identifier.
        Task<User> GetUserByIdAsync(int userId);

        // Retrieves a user by their username.
        Task<User> GetUserByUsernameAsync(string username);

        // Retrieves a user by their email.
        Task<User> GetUserByEmailAsync(string email);

        // Retrieves a list of all users.
        Task<List<User>> GetAllUsersAsync();

        Task<string> GetProfilePictureUrl(int userId);

        // Retrieves jobs associated with a user by their unique identifier.
        Task<List<Job>> GetJobsByUserIdAsync(int userId);

        // Retrieves reviews associated with a user by their unique identifier.
        Task<List<ReviewUser>> GetReviewsByUserIdAsync(int userId);

        // Retrieves certificates associated with a user by their unique identifier.
        Task<List<CertificatUser>> GetUserCertificatesByUserIdAsync(int userId);

        Task<List<CertificatUser>> GetAllCertificatesAsync();

        // Retrieves badges associated with a user by their unique identifier.
        Task<List<UserBadge>> GetUserBadgesByUserIdAsync(int userId);

        // Creates a new user in the service with a plain text password.
        Task CreateUserWithPlainTextPasswordAsync(User user, string plainTextPassword);

        // Updates an existing user in the service.
        Task UpdateUserAsync(User updatedUser, string plainTextPassword);

        // Deletes a user by their unique identifier from the service.
        Task DeleteUserAsync(int userId);

        Task CreateCertificatUserAsync(CertificatUser certificatUser);
    }
}
