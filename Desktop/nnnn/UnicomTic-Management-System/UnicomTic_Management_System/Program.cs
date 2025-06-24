using System;
using System.Windows.Forms;
using UnicomTic_Management_System.Data;
using UnicomTic_Management_System.View;

namespace UnicomTic_Management_System
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize database
            try
            {
                Migration.CreateTable();
                DatabaseManager.SeedDefaultData();
                Console.WriteLine("Database initialized successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database initialization failed: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Run the login form (replace Loginform with your actual form class name)
            Application.Run(new Loginform());
            //Application.Run(new Dashboard());
        }
    }
}
