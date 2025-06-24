using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using UnicomTic_Management_System.Model;

namespace UnicomTic_Management_System.Controller
{
    public class LoginController
    {
        private readonly string _connectionString;

        public LoginController(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Get all users (without passwords)
        public List<User> GetAllUsers()
        {
            var users = new List<User>();

            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            string query = "SELECT Id, Username, Role FROM Users";

            using var cmd = new SQLiteCommand(query, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                users.Add(new User
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Username = reader["Username"].ToString() ?? string.Empty,
                    Role = Enum.TryParse<UserRole>(reader["Role"].ToString(), out var role) ? role : UserRole.Student
                });
            }

            return users;
        }

        // Add new user with password hashing
        public User AddUser(User user, string plainPassword)
        {
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            string query = "INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, @Role); " +
                           "SELECT last_insert_rowid();";

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword);

            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Password", hashedPassword);
            cmd.Parameters.AddWithValue("@Role", user.Role.ToString());

            user.Id = Convert.ToInt32(cmd.ExecuteScalar());
            user.PasswordHash = hashedPassword;

            return user;
        }

        // Verify credentials for login
        public bool Login(Credentials credentials)
        {
            try
            {
                using var conn = new SQLiteConnection(_connectionString);
                conn.Open();

                string query = "SELECT Password FROM Users WHERE Username = @Username";

                using var cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", credentials.Username);

                var storedHash = cmd.ExecuteScalar() as string;

                if (storedHash == null)
                    return false;

                return BCrypt.Net.BCrypt.Verify(credentials.Password, storedHash);
            }
            catch
            {
                return false;
            }
        }

        // Get user role after successful login
        public UserRole? GetUserRole(string username)
        {
            try
            {
                using var conn = new SQLiteConnection(_connectionString);
                conn.Open();

                string query = "SELECT Role FROM Users WHERE Username = @Username";

                using var cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);

                var roleString = cmd.ExecuteScalar() as string;

                if (roleString != null && Enum.TryParse<UserRole>(roleString, out var role))
                    return role;

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
