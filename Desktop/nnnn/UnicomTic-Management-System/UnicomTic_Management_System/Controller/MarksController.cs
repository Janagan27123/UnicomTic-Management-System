using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using UnicomTic_Management_System.Model;

namespace UnicomTic_Management_System.Controller
{
    public class MarksController
    {
        private readonly string _connectionString;

        public MarksController(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Marks> GetAllMarks()
        {
            var marks = new List<Marks>();

            using (var conn = new SQLiteConnection(_connectionString))
            {
                string query = "SELECT * FROM Marks";
                var cmd = new SQLiteCommand(query, conn);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        marks.Add(new Marks
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            StudentId = Convert.ToInt32(reader["StudentId"]),
                            ExamId = Convert.ToInt32(reader["ExamId"]),
                            MarksValue = Convert.ToInt32(reader["Marksvalue"])
                        });
                    }
                }
            }

            return marks;
        }

        public Marks GetMarkById(int id)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                string query = "SELECT * FROM Marks WHERE Id = @Id";
                var cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Marks
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            StudentId = Convert.ToInt32(reader["StudentId"]),
                            ExamId = Convert.ToInt32(reader["ExamId"]),
                            MarksValue = Convert.ToInt32(reader["Marksvalue"])
                        };
                    }
                }
            }
            return null;
        }

        public bool AddMark(Marks mark)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                string query = "INSERT INTO Marks (StudentId, ExamId, Score) VALUES (@StudentId, @ExamId, @Score)";
                var cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@StudentId", mark.StudentId);
                cmd.Parameters.AddWithValue("@ExamId", mark.ExamId);
                cmd.Parameters.AddWithValue("@Score", mark.MarksValue);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateMark(Marks mark)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                string query = "UPDATE Marks SET StudentId = @StudentId, ExamId = @ExamId, Score = @Score WHERE Id = @Id";
                var cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@StudentId", mark.StudentId);
                cmd.Parameters.AddWithValue("@ExamId", mark.ExamId);
                cmd.Parameters.AddWithValue("@Score", mark.MarksValue);
                cmd.Parameters.AddWithValue("@Id", mark.Id);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteMark(int id)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                string query = "DELETE FROM Marks WHERE Id = @Id";
                var cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
