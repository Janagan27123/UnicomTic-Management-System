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
    public class CourseController
    {
        //public void AddCourse(Course course) => CourseRepository.Add(course);
        //public void UpdateCourse(Course course) => CourseRepository.Update(course);
        //public void DeleteCourse(int id) => CourseRepository.Delete(id);
        //public List<Course> GetAllCourses() => CourseRepository.GetAll();
        public void Insert(Course course)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO Courses (CourseName, Description) VALUES (@name, @desc)";
                cmd.Parameters.AddWithValue("@name", course.CourseName);
                //cmd.Parameters.AddWithValue("@desc", course.Description);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Course> GetAll()
        {
            var list = new List<Course>();
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT * FROM Courses", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Course
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            CourseName = reader["CourseName"].ToString(),
                            //Description = reader["Description"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public void Delete(int courseId)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM Courses WHERE Id = @id";
                cmd.Parameters.AddWithValue("@id", courseId);
                cmd.ExecuteNonQuery();
            }
        }

        private CourseController repo = new CourseController();

        public void AddCourse(Course course)
        {
            if (!string.IsNullOrWhiteSpace(course.CourseName))
            {
                repo.Insert(course);
            }
            else
            {
                throw new ArgumentException("Course name cannot be empty");
            }
        }

        public List<Course> GetCourses()
        {
            return repo.GetAll();
        }

        public void DeleteCourse(int courseId)
        {
            repo.Delete(courseId);
        }


    }
}
