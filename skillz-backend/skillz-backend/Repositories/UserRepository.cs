using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using skillz_backend.data;
using skillz_backend.models;

namespace skillz_backend.Repositories
{
    // Repository implementation for user-related operations.
    public class UserRepository : IUserRepository
    {
        private readonly SkillzDbContext _dbContext;

        // Constructor that initializes the repository with the provided DbContext.
        public UserRepository(SkillzDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        // Retrieves a user by their unique identifier.
        public async Task<User> GetUserByIdAsync(int userId)
        {
            // Asynchronously retrieves a user by ID from the database using EF Core's FindAsync.
            return await _dbContext.Users.FindAsync(userId);
        }

        // Retrieves a user by their username.
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            // Asynchronously retrieves a user by their username from the database using EF Core's SingleOrDefaultAsync and a predicate.
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        // Retrieves a user by their email address.
        public async Task<User> GetUserByEmailAsync(string email)
        {
            // Asynchronously retrieves a user by their email address from the database using EF Core's SingleOrDefaultAsync and a predicate.
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        // Retrieves all users from the database.
        public async Task<List<User>> GetAllUsersAsync()
        {
            // Asynchronously retrieves all users from the database using EF Core's ToListAsync.
            return await _dbContext.Users.ToListAsync();
        }

        // Retrieves the location of a user based on their user ID.
        public async Task<string> GetUserLocationByIdAsync(int userId)
        {
            // Asynchronously retrieves a user by ID from the database using EF Core's FindAsync.
            var user = await _dbContext.Users.FindAsync(userId);
            return user?.Location;
        }

        public async Task<string> GetProfilePictureUrlAsync(int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            return user?.ProfilePicture;
        }

        // Retrieves jobs associated with a user.
        public async Task<List<Job>> GetJobsByUserIdAsync(int userId)
        {
            // Asynchronously retrieves a user by ID with associated jobs using EF Core's Include and FirstOrDefaultAsync.
            var user = await _dbContext.Users
                .Include(u => u.Jobs)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            // Returns the associated jobs or an empty list if the user is not found.
            return user?.Jobs ?? new List<Job>();
        }

        // Retrieves reviews associated with a user.
        public async Task<List<ReviewUser>> GetReviewsByUserIdAsync(int userId)
        {
            // Asynchronously retrieves a user by ID with associated reviews using EF Core's Include and FirstOrDefaultAsync.
            var user = await _dbContext.Users
                .Include(u => u.ReviewsUser)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            // Returns the associated reviews or an empty list if the user is not found.
            return user?.ReviewsUser ?? new List<ReviewUser>();
        }

        // Retrieves certificates associated with a user.
        public async Task<List<CertificatUser>> GetUserCertificatesByUserIdAsync(int userId)
        {
            // Asynchronously retrieves a user by ID with associated certificates using EF Core's Include and FirstOrDefaultAsync.
            var userCertificates = await _dbContext.CertificatsUser
            .Where(c => c.IdUser == userId)
            .ToListAsync();

            // Returns the associated certificates or an empty list if the user is not found.
            return userCertificates;
        }

        public async Task<List<CertificatUser>> GetAllCertificatesAsync()
        {
            return await _dbContext.CertificatsUser.ToListAsync();
        }

        // Retrieves badges associated with a user.
        public async Task<List<UserBadge>> GetUserBadgesByUserIdAsync(int userId)
        {
            // Asynchronously retrieves a user by ID with associated badges using EF Core's Include and FirstOrDefaultAsync.
            var user = await _dbContext.Users
                .Include(u => u.UserBadges)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            // Returns the associated badges or an empty list if the user is not found.
            return user?.UserBadges ?? new List<UserBadge>();
        }

        // Creates a new user in the database.
        public async Task CreateUserAsync(User user)
        {
            // Adds the user to the DbContext and asynchronously saves changes to the database.
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        // Updates an existing user in the database.
        public async Task UpdateUserAsync(User user)
        {
            // Marks the user entity as modified in the DbContext and asynchronously saves changes to the database.
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        // Deletes a user based on their user ID from the database.
        public async Task DeleteUserAsync(int userId)
        {
            // Finds the user by ID using EF Core's FindAsync.
            var userToDelete = await _dbContext.Users.FindAsync(userId);

            // Removes the user from the DbContext and asynchronously saves changes to the database if the user is found.
            if (userToDelete != null)
            {
                _dbContext.Users.Remove(userToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task CreateCertificatUserAsync(CertificatUser certificatUser)
        {
            _dbContext.CertificatsUser.Add(certificatUser);
            await _dbContext.SaveChangesAsync();
        }
    }
}
