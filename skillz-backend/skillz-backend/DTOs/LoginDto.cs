namespace skillz_backend.DTOs
{
    // Represents data for user login.
    public class LoginDto
    {
        public int Id { get; set; }
        // Gets or sets the username.
        public string Username { get; set; }

        public string Email { get; set; }

        // Gets or sets the password.
        public string Password { get; set; }
    }
}
