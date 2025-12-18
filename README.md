# SingkoFitnessWebApi ✅

![.NET](https://img.shields.io/badge/.NET-8.0-blue) ![License](https://img.shields.io/badge/License-MIT-lightgrey)

## About this project 💡
SingkoFitnessWebApi is an ASP.NET Core Web API providing backend services for a fitness application. It implements CRUD endpoints for users, exercises, workouts, nutrition logs, and progress logs, and includes an AI endpoint for fitness-related queries.

## Key features ✨
- RESTful endpoints for Users, Exercises, Workouts, Nutrition Logs, Progress Logs
- AI query endpoint (`SingkoFitnessAiController`) for fitness suggestions and Q&A
- Entity Framework Core (`SingkoFitnessWebDbContext`) for data access
- AutoMapper profiles for DTO <-> Model mapping
- Basic Razor Views for user management (under `Views/Users`)

## Project structure 🔧
- `Controllers/` — API controllers
- `Dtos/` — Data Transfer Objects
- `Models/` — Entity models and DbContext
- `Profiles/` — AutoMapper profiles
- `Views/` — Simple Razor views
- `Program.cs` — App startup/config
- `appsettings.json` / `appsettings.Development.json` — Configuration

## Prerequisites ✔️
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- A database supported by EF Core (configure connection string in `appsettings.json`)

## Run locally (Windows PowerShell) 🖥️
```powershell
cd <repository_root>\SingkoFItnessWebApi
dotnet restore
dotnet build
dotnet run
```

> Tip: Update the database connection string in `appsettings.json` or `appsettings.Development.json` before running and apply migrations if needed:
```powershell
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## API usage / Testing 🧪
- Use Postman, curl, or Swagger (if enabled) to test endpoints.
- Example (curl):
```bash
curl -X GET "https://localhost:5001/api/Exercise" -H "accept: application/json"
```

## Contributing 🤝
1. Fork the repo
2. Create a feature branch
3. Implement changes and add tests
4. Open a pull request for review

## License 📜
This project includes a placeholder for an MIT license. Replace or add a `LICENSE` file as needed.

## Contact
For issues or questions, open an issue in the repository or contact the maintainers.
