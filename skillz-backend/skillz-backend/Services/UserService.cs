using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using skillz_backend.data;
using skillz_backend.models;
using skillz_backend.Repositories;
using skillz_backend.Repositories.Interfaces;

namespace skillz_backend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        // Constructor to initialize the user repository
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        // Retrieves a user by ID asynchronously
        public async Task<User> GetUserByIdAsync(int userId)
        {
            // Validation for a positive UserId
            if (userId <= 0)
            {
                throw new ArgumentException("UserId should be a positive integer.");
            }

            return await _userRepository.GetUserByIdAsync(userId);
        }

        // Retrieves a user by username asynchronously
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            // Validation for a non-null and non-empty username
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username cannot be null or empty.");
            }

            return await _userRepository.GetUserByUsernameAsync(username);
        }

        // Retrieves a user by email address asynchronously
        public async Task<User> GetUserByEmailAsync(string email)
        {
            // Validation for a valid email address
            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
            {
                throw new ArgumentException("Invalid email address.");
            }

            return await _userRepository.GetUserByEmailAsync(email);
        }

        // Retrieves jobs associated with a user ID asynchronously
        public async Task<List<Job>> GetJobsByUserIdAsync(int userId)
        {
            // Validation for a positive UserId
            if (userId <= 0)
            {
                throw new ArgumentException("UserId should be a positive integer.");
            }

            return await _userRepository.GetJobsByUserIdAsync(userId);
        }

        // Retrieves reviews associated with a user ID asynchronously
        public async Task<List<ReviewUser>> GetReviewsByUserIdAsync(int userId)
        {
            // Validation for a positive UserId
            if (userId <= 0)
            {
                throw new ArgumentException("UserId should be a positive integer.");
            }

            return await _userRepository.GetReviewsByUserIdAsync(userId);
        }

        // Retrieves certificates associated with a user ID asynchronously
        public async Task<List<CertificatUser>> GetUserCertificatesByUserIdAsync(int userId)
        {
            // Validation for a positive UserId
            if (userId <= 0)
            {
                throw new ArgumentException("UserId should be a positive integer.");
            }

            return await _userRepository.GetUserCertificatesByUserIdAsync(userId);
        }

        public async Task<List<CertificatUser>> GetAllCertificatesAsync()
        {
            return await _userRepository.GetAllCertificatesAsync();
        }

        // Retrieves all users asynchronously
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        // Retrieves badges associated with a user ID asynchronously
        public async Task<List<UserBadge>> GetUserBadgesByUserIdAsync(int userId)
        {
            // Validation for a positive UserId
            if (userId <= 0)
            {
                throw new ArgumentException("UserId should be a positive integer.");
            }

            return await _userRepository.GetUserBadgesByUserIdAsync(userId);
        }

        public async Task<string> GetProfilePictureUrl(int userId)
        {
            return await _userRepository.GetProfilePictureUrlAsync(userId);
        }

        // Creates a new user with a plain text password asynchronously
        public async Task CreateUserWithPlainTextPasswordAsync(User user, string plainTextPassword)
        {
            // Validation for a non-null user object
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User object cannot be null.");
            }

            // Check if a user with the same UserId, username, or email already exists
            var existingUserById = await _userRepository.GetUserByIdAsync(user.UserId);
            if (existingUserById != null)
            {
                throw new InvalidOperationException("A user with the same UserId already exists.");
            }

            var existingUserByUsername = await _userRepository.GetUserByUsernameAsync(user.Username);
            if (existingUserByUsername != null)
            {
                throw new InvalidOperationException("A user with the same username already exists.");
            }

            var existingUserByEmail = await _userRepository.GetUserByEmailAsync(user.Email);
            if (existingUserByEmail != null)
            {
                throw new InvalidOperationException("A user with the same email address already exists.");
            }

            // Hash the plain text password and set salt and hash in the user object
            using var hmac = new HMACSHA512();
            user.PasswordSalt = hmac.Key;
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(plainTextPassword));

            // Create the user
            await _userRepository.CreateUserAsync(user);
        }

        // Updates an existing user asynchronously with a new plain text password
        public async Task UpdateUserAsync(User user, string plainTextPassword)
        {
            // Validation for a valid user object and UserId
            if (user == null || user.UserId <= 0)
            {
                throw new ArgumentException("Invalid user object or UserId.");
            }

            // Check if a user with the same email already exists (excluding the current user)
            var existingUserByEmail = await _userRepository.GetUserByEmailAsync(user.Email);

            if (existingUserByEmail != null && existingUserByEmail.UserId != user.UserId)
            {
                throw new InvalidOperationException("A user with the same email address already exists.");
            }

            // Hash the plain text password and update salt and hash in the user object
            using var hmac = new HMACSHA512();
            user.PasswordSalt = hmac.Key;
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(plainTextPassword));

            // Update the user
            await _userRepository.UpdateUserAsync(user);
        }

        // Deletes a user by ID asynchronously
        public async Task DeleteUserAsync(int userId)
        {
            // Validation for a positive UserId
            if (userId <= 0)
            {
                throw new ArgumentException("UserId should be a positive integer.");
            }

            // Delete the user
            await _userRepository.DeleteUserAsync(userId);
        }

         public async Task CreateCertificatUserAsync(CertificatUser certificatUser)
        {
            await _userRepository.CreateCertificatUserAsync(certificatUser);
        }
    }
}
