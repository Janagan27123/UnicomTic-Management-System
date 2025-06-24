using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace UnicomTic_Management_System.Data
{
    internal static class Migration
    {
        public static void CreateTable()
        {
            using (var getDbconn = DatabaseManager.GetConnection())
            {


                string Createtable = @"
                CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Username TEXT NOT NULL,
                Password TEXT NOT NULL,
                Role TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Courses (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                CourseName TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Students (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Address TEXT NOT NULL,
                NIC TEXT NOT NULL,
                CourseId INTEGER,
                FOREIGN KEY (CourseId) REFERENCES Courses(Id)
                );

                CREATE TABLE IF NOT EXISTS Subjects (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                CourseId INTEGER,
                FOREIGN KEY (CourseId) REFERENCES Courses(Id)
                );

                CREATE TABLE IF NOT EXISTS Exams (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ExamName TEXT NOT NULL,
                Date TEXT,
                SubjectId INTEGER,
                FOREIGN KEY (SubjectId) REFERENCES Subjects(Id)
                );

                CREATE TABLE IF NOT EXISTS Marks (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                StudentId INTEGER,
                ExamId INTEGER,
                MarksValue INTEGER,
                FOREIGN KEY (StudentId) REFERENCES Students(Id),
                FOREIGN KEY (ExamId) REFERENCES Exams(Id)
                );

                CREATE TABLE IF NOT EXISTS Rooms (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                RoomNumber TEXT NOT NULL,
                Capacity INTEGER
                );

                CREATE TABLE IF NOT EXISTS Timetables (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                SubjectId INTEGER,
                RoomId INTEGER,
                Day TEXT,
                StartTime TEXT,
                EndTime TEXT,
                FOREIGN KEY (SubjectId) REFERENCES Subjects(Id),
                FOREIGN KEY (RoomId) REFERENCES Rooms(Id)
                );
                ";
                SQLiteCommand cmd = new SQLiteCommand(Createtable, getDbconn);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
