using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using skillz_backend.data;
using skillz_backend.models;

namespace skillz_backend.Repositories
{
    // Interface defining operations related to the User entity in the repository.
    public interface IUserRepository
    {
        // Retrieves a user by their unique identifier.
        Task<User> GetUserByIdAsync(int userId);

        // Retrieves a user by their username.
        Task<User> GetUserByUsernameAsync(string username);

        // Retrieves a user by their email address.
        Task<User> GetUserByEmailAsync(string email);

        // Retrieves the location of a user by their unique identifier.
        Task<string> GetUserLocationByIdAsync(int userId);

        // Retrieves a list of all users.
        Task<List<User>> GetAllUsersAsync();

        // Retrieves jobs associated with a user by their unique identifier.
        Task<List<Job>> GetJobsByUserIdAsync(int userId);

        // Retrieves reviews associated with a user by their unique identifier.
        Task<List<ReviewUser>> GetReviewsByUserIdAsync(int userId);

        // Retrieves certificates associated with a user by their unique identifier.
        Task<List<CertificatUser>> GetUserCertificatesByUserIdAsync(int userId);

        Task<List<CertificatUser>> GetAllCertificatesAsync();

        // Retrieves badges associated with a user by their unique identifier.
        Task<List<UserBadge>> GetUserBadgesByUserIdAsync(int userId);

        Task<string> GetProfilePictureUrlAsync(int userId);

        // Creates a new user in the repository.
        Task CreateUserAsync(User user);

        // Updates an existing user in the repository.
        Task UpdateUserAsync(User user);

        // Deletes a user by their unique identifier from the repository.
        Task DeleteUserAsync(int userId);

        Task CreateCertificatUserAsync(CertificatUser certificatUser);
    }
}
