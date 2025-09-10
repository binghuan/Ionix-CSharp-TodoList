# Todo API - C# Web API Server

**English | [繁體中文](README.md)**

This is the backend API of the Todo List application, built with .NET 8 Web API and Entity Framework Core.

## Installation and Setup

### Prerequisites

- .NET 8 SDK
- Visual Studio 2022 or VS Code (optional)

### Install and Start

```bash
# Restore NuGet packages
dotnet restore

# Start development server
dotnet run
```

The API will start at the following endpoints:
- HTTP: `http://localhost:7000`
- HTTPS: `https://localhost:7001`
- Swagger UI: `https://localhost:7001/swagger`

## Project Structure

```
server/
├── Controllers/         # API controllers
├── Data/               # Database context
├── DTOs/               # Data transfer objects
├── Models/             # Data models
├── Properties/         # Launch settings
├── appsettings.json    # Application settings
└── Program.cs          # Application entry point
```

## API Endpoints

### Todo Management

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/todos` | Get all Todo items |
| GET | `/api/todos/{id}` | Get specific Todo item |
| POST | `/api/todos` | Create new Todo item |
| PUT | `/api/todos/{id}` | Update Todo item |
| DELETE | `/api/todos/{id}` | Delete Todo item |

### Request Examples

#### Create Todo
```http
POST /api/todos
Content-Type: application/json

{
  "title": "Learn Ionic",
  "description": "Complete Ionic Angular tutorial"
}
```

#### Update Todo
```http
PUT /api/todos/1
Content-Type: application/json

{
  "title": "Updated Title",
  "isCompleted": true
}
```

## Data Models

### Todo Model
```csharp
public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
```

## Configuration

### Database Settings

Currently uses In-Memory Database, suitable for development and testing. To use a real database, modify `Program.cs`:

```csharp
// Use SQL Server
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlServer(connectionString));

// Use SQLite
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlite(connectionString));
```

### CORS Configuration

CORS is configured to allow requests from frontend applications:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:8100", "http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});
```

## Development Tools

### Swagger UI
Visit `https://localhost:7001/swagger` to view and test the API.

### Entity Framework Tools

```bash
# Install EF tools
dotnet tool install --global dotnet-ef

# Create migration (if using real database)
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update
```

## Build and Deployment

### Development Build
```bash
dotnet build
```

### Publish Application
```bash
# Publish as self-contained application
dotnet publish -c Release --self-contained

# Publish as framework-dependent application
dotnet publish -c Release
```

### Docker Deployment

Create `Dockerfile`:
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY bin/Release/net8.0/publish/ ./
ENTRYPOINT ["dotnet", "TodoApi.dll"]
```

## Security Considerations

- Authentication and authorization are not currently implemented
- For production environments, consider adding:
  - JWT authentication
  - API keys
  - Rate limiting
  - HTTPS enforcement

## Troubleshooting

### Common Issues

1. **CORS Error**: Check CORS policy settings
2. **Port Conflict**: Modify ports in `launchSettings.json`
3. **SSL Certificate Issues**: Run `dotnet dev-certs https --trust`

### Logging Configuration

Modify `appsettings.json` to adjust logging levels:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  }
}
```
