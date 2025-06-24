using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using UnicomTic_Management_System.Model;


namespace UnicomTic_Management_System.Controller
{
    public class TimetableController
    {
        private readonly string _connectionString;

        public TimetableController(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Timetable> GetAllTimetables()
        {
            var timetables = new List<Timetable>();

            using (var conn = new SQLiteConnection(_connectionString))
            {
                string query = "SELECT * FROM Timetables";
                var cmd = new SQLiteCommand(query, conn);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        timetables.Add(new Timetable
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            SubjectId = Convert.ToInt32(reader["SubjectId"]),
                            RoomId = Convert.ToInt32(reader["RoomId"]),
                            Day = reader["Day"].ToString() ?? string.Empty,
                            StartTime = TimeSpan.TryParse(reader["StartTime"].ToString(), out var startTime) ? startTime : TimeSpan.Zero,
                            EndTime = TimeSpan.TryParse(reader["EndTime"].ToString(), out var endTime) ? endTime : TimeSpan.Zero
                        });
                    }
                }
            }

            return timetables;
        }

        public Timetable GetTimetableById(int id)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                string query = "SELECT * FROM Timetables WHERE Id = @Id";
                var cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Timetable
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            SubjectId = Convert.ToInt32(reader["SubjectId"]),
                            RoomId = Convert.ToInt32(reader["RoomId"]),
                            Day = reader["Day"].ToString() ?? string.Empty,
                            StartTime = TimeSpan.TryParse(reader["StartTime"].ToString(), out var startTime) ? startTime : TimeSpan.Zero,
                            EndTime = TimeSpan.TryParse(reader["EndTime"].ToString(), out var endTime) ? endTime : TimeSpan.Zero
                        };
                    }
                }
            }
            return null;
        }

        public bool AddTimetable(Timetable timetable)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                string query = @"
                INSERT INTO Timetables (SubjectId, RoomId, Day, StartTime, EndTime)
                VALUES (@SubjectId, @RoomId, @Day, @StartTime, @EndTime)";
                var cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@SubjectId", timetable.SubjectId);
                cmd.Parameters.AddWithValue("@RoomId", timetable.RoomId);
                cmd.Parameters.AddWithValue("@Day", timetable.Day);
                cmd.Parameters.AddWithValue("@StartTime", timetable.StartTime.ToString());
                cmd.Parameters.AddWithValue("@EndTime", timetable.EndTime.ToString());

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateTimetable(Timetable timetable)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                string query = @"
                UPDATE Timetables SET
                    SubjectId = @SubjectId,
                    RoomId = @RoomId,
                    Day = @Day,
                    StartTime = @StartTime,
                    EndTime = @EndTime
                WHERE Id = @Id";
                var cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@SubjectId", timetable.SubjectId);
                cmd.Parameters.AddWithValue("@RoomId", timetable.RoomId);
                cmd.Parameters.AddWithValue("@Day", timetable.Day);
                cmd.Parameters.AddWithValue("@StartTime", timetable.StartTime.ToString());
                cmd.Parameters.AddWithValue("@EndTime", timetable.EndTime.ToString());
                cmd.Parameters.AddWithValue("@Id", timetable.Id);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteTimetable(int id)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                string query = "DELETE FROM Timetables WHERE Id = @Id";
                var cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
