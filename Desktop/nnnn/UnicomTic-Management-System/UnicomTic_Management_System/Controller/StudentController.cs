using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using UnicomTic_Management_System.Data;
using UnicomTic_Management_System.Model;

namespace UnicomTic_Management_System.Controller
{
    internal class StudentController
    {
        public string AddStudent(Student student)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                string addStudentQuery = "INSERT INTO Students (Name, Address) VALUES (@name, @address)";

                SQLiteCommand insertStudentCommand = new SQLiteCommand(addStudentQuery, conn);
                insertStudentCommand.Parameters.AddWithValue("@name", student.Name);
                insertStudentCommand.Parameters.AddWithValue("@address", student.Address);
                insertStudentCommand.ExecuteNonQuery();
            }

            return "Student Added Successfully!";
        }

        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();

            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open(); //  Required!

                string query = "SELECT * FROM Students";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Address = reader.GetString(2)
                        });
                    }
                }
            }

            return students;
        }
        public void UpdateStudent(Student student)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE Students SET Name = @name, Address = @address WHERE Id = @id";
                cmd.Parameters.AddWithValue("@name", student.Name);
                cmd.Parameters.AddWithValue("@address", student.Address);
                cmd.Parameters.AddWithValue("@id", student.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteStudent(int id)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM Students WHERE Id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }



    }

}


