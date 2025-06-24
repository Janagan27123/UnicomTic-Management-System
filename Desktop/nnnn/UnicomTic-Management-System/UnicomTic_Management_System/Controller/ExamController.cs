using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using UnicomTic_Management_System.Model;

namespace UnicomTic_Management_System.Controller
{
    public class ExamController
    {
        private readonly string _connectionString;

        public ExamController(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Exam> GetAllExams()
        {
            List<Exam> exams = new List<Exam>();

            using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
            {
                string query = "SELECT * FROM Exams";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                conn.Open();

                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    exams.Add(new Exam
                    {
                        Id = (int)reader["Id"],
                        ExamName = reader["ExamName"].ToString(),
                        Date = Convert.ToDateTime(reader["Date"]),
                        SubjectId = (int)reader["SubjectId"]
                    });
                }
            }

            return exams;
        }

        public bool AddExam(Exam exam)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
            {
                string query = "INSERT INTO Exams (ExamName, Date, SubjectId) VALUES (@ExamName, @Date, @SubjectId)";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                //cmd.Parameters.AddWithValue("@ExamName", exam.ExamName);
                cmd.Parameters.AddWithValue("@Date", exam.Date);
                cmd.Parameters.AddWithValue("@SubjectId", exam.SubjectId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateExam(Exam exam)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
            {
                string query = "UPDATE Exams SET ExamName = @ExamName, Date = @Date, SubjectId = @SubjectId WHERE Id = @Id";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                //cmd.Parameters.AddWithValue("@ExamName", exam.ExamName);
                cmd.Parameters.AddWithValue("@Date", exam.Date);
                cmd.Parameters.AddWithValue("@SubjectId", exam.SubjectId);
                cmd.Parameters.AddWithValue("@Id", exam.Id);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteExam(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
            {
                string query = "DELETE FROM Exams WHERE Id = @Id";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public Exam GetExamById(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
            {
                string query = "SELECT * FROM Exams WHERE Id = @Id";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Exam
                    {
                        Id = (int)reader["Id"],
                        //ExamName = reader["ExamName"].ToString(),
                        Date = Convert.ToDateTime(reader["Date"]),
                        SubjectId = (int)reader["SubjectId"]
                    };
                }
            }

            return null;
        }
    }
}
