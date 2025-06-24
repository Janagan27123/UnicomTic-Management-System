# UnicomTic Management System

A comprehensive educational management system built with C# WinForms and SQLite database.

## Features

- **User Authentication**: Role-based login system (Admin, Student, Staff, Lecturer)
- **Student Management**: Add, edit, delete, and view student information
- **Course Management**: Manage courses and subjects
- **Exam Management**: Create and manage exams
- **Marks Management**: Record and view student marks
- **Timetable Management**: Create and manage class schedules
- **Dashboard**: Role-specific dashboards for different user types

## Technology Stack

- **Framework**: .NET Framework 4.8
- **Language**: C# 8.0
- **Database**: SQLite
- **UI**: Windows Forms
- **ORM**: Entity Framework 6.5.1
- **Security**: BCrypt for password hashing

## Project Structure

```
UnicomTic_Management_System/
├── Controller/          # Business logic controllers
├── Data/               # Database management and migrations
├── Model/              # Data models and entities
├── View/               # Windows Forms UI
├── Properties/         # Project properties and resources
└── packages/           # NuGet packages
```

## Installation

1. Clone the repository
2. Open `UnicomTic_Management_System.sln` in Visual Studio
3. Restore NuGet packages
4. Build the solution
5. Run the application

## Default Credentials

- **Username**: admin
- **Password**: admin123
- **Role**: Admin

## Database

The application uses SQLite database (`management.db`) which is automatically created on first run. The database includes tables for:

- Users
- Students
- Courses
- Subjects
- Exams
- Marks
- Timetables
- Rooms

## Usage

1. Launch the application
2. Login with appropriate credentials
3. Navigate through the dashboard based on your role
4. Use the various management modules as needed

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

## License

This project is licensed under the MIT License.

## Support

For support and questions, please contact the development team. 