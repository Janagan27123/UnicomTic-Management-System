using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using UnicomTic_Management_System.Model;
using BCrypt.Net;



namespace UnicomTic_Management_System.Controller
{
    public class AdminController
    {
        private readonly string _connectionString;

        public AdminController(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Create default admin user with proper ID and password
        public bool CreateDefaultAdmin()
        {
            try
            {
                using var conn = new SQLiteConnection(_connectionString);
                conn.Open();

                // Check if admin already exists
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = 'admin'";
                using var checkCmd = new SQLiteCommand(checkQuery, conn);
                int existingCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (existingCount > 0)
                {
                    Console.WriteLine("Admin user already exists.");
                    return true;
                }

                // Create admin user with hashed password
                string insertQuery = @"
                    INSERT INTO Users (Username, Password, Role) 
                    VALUES (@Username, @Password, @Role);
                    SELECT last_insert_rowid();";

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword("admin123");

                using var cmd = new SQLiteCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@Username", "admin");
                cmd.Parameters.AddWithValue("@Password", hashedPassword);
                cmd.Parameters.AddWithValue("@Role", "Admin");

                int adminId = Convert.ToInt32(cmd.ExecuteScalar());
                Console.WriteLine($"Admin user created successfully with ID: {adminId}");
                Console.WriteLine("Default credentials: admin/admin123");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating admin user: {ex.Message}");
                return false;
            }
        }

        // Get admin user details
        public User GetAdminUser()
        {
            try
            {
                using var conn = new SQLiteConnection(_connectionString);
                conn.Open();

                string query = "SELECT Id, Username, Role FROM Users WHERE Username = 'admin'";
                using var cmd = new SQLiteCommand(query, conn);
                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new User
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Username = reader["Username"].ToString() ?? string.Empty,
                        Role = Enum.TryParse<UserRole>(reader["Role"].ToString(), out var role) ? role : UserRole.Admin
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting admin user: {ex.Message}");
            }
            return null;
        }

        // Change admin password
        public bool ChangeAdminPassword(string newPassword)
        {
            try
            {
                using var conn = new SQLiteConnection(_connectionString);
                conn.Open();

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
                string query = "UPDATE Users SET Password = @Password WHERE Username = 'admin'";

                using var cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@Password", hashedPassword);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Admin password changed successfully.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error changing admin password: {ex.Message}");
            }
            return false;
        }

        // Get all users (admin only)
        public List<User> GetAllUsers()
        {
            var users = new List<User>();

            try
            {
                using var conn = new SQLiteConnection(_connectionString);
                conn.Open();

                string query = "SELECT Id, Username, Role FROM Users ORDER BY Id";

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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting users: {ex.Message}");
            }

            return users;
        }

        // Create new user
        public bool CreateUser(string username, string password, UserRole role)
        {
            try
            {
                using var conn = new SQLiteConnection(_connectionString);
                conn.Open();

                // Check if username already exists
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                using var checkCmd = new SQLiteCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@Username", username);
                int existingCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (existingCount > 0)
                {
                    Console.WriteLine($"Username '{username}' already exists.");
                    return false;
                }

                // Create new user
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                string insertQuery = @"
                    INSERT INTO Users (Username, Password, Role) 
                    VALUES (@Username, @Password, @Role);
                    SELECT last_insert_rowid();";

                using var cmd = new SQLiteCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", hashedPassword);
                cmd.Parameters.AddWithValue("@Role", role.ToString());

                int userId = Convert.ToInt32(cmd.ExecuteScalar());
                Console.WriteLine($"User '{username}' created successfully with ID: {userId}");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating user: {ex.Message}");
                return false;
            }
        }

        // Delete user
        public bool DeleteUser(int userId)
        {
            try
            {
                using var conn = new SQLiteConnection(_connectionString);
                conn.Open();

                string query = "DELETE FROM Users WHERE Id = @Id";
                using var cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", userId);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine($"User with ID {userId} deleted successfully.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user: {ex.Message}");
            }
            return false;
        }
    }
}
