# Todo List Application

**English | [繁體中文](README_zh.md)**

A Todo List application built with Ionic Angular as the frontend and C# Web API as the backend.

## Project Structure

```
Ionix-CSharp-TodoList/
├── client/          # Ionic Angular frontend app
└── server/          # C# Web API backend service
```

## Features

- ✅ Add, edit, delete Todo items
- ✅ Mark Todo items as completed/incomplete
- ✅ Responsive design for mobile and desktop
- ✅ Real-time data synchronization
- ✅ Pull-to-refresh functionality
- ✅ Swipe-to-delete functionality

## Environment Setup

Before getting started, make sure your development environment has the following tools installed:

### Prerequisites

1. **Node.js** (version 16 or higher)
   - Download and install: https://nodejs.org/
   - Verify installation: `node --version && npm --version`

2. **.NET 8 SDK**
   - Download and install: https://dotnet.microsoft.com/download
   - Verify installation: `dotnet --version`

### Install Ionic CLI

Ionic CLI is a command-line tool for developing Ionic applications and must be installed globally:

```bash
# Install Ionic CLI globally
npm install -g @ionic/cli

# Verify successful installation
ionic --version
```

> **Note**: If you're using macOS or Linux and encounter permission issues, you may need to use `sudo`:
> ```bash
> sudo npm install -g @ionic/cli
> ```

After installation, you should see the Ionic CLI version number (e.g., 7.2.1).

## Quick Start

### 1. Start Backend Server

```bash
cd server
dotnet run
```

The backend server will start at `https://localhost:7001` and `http://localhost:7000`.

### 2. Start Frontend Application

⚠️ **Important**: Frontend commands must be run in the `client` directory

```bash
# Navigate to frontend directory
cd client

# Install dependencies
npm install

# Start Ionic development server
ionic serve
```

The frontend application will start at `http://localhost:8100`.

> **Troubleshooting**: If you encounter the error "ionic serve can only be run in an Ionic project directory", make sure you're running the `ionic serve` command in the `client` directory.

### 3. Mobile Development (Optional)

For building and testing on Android/iOS devices:

📖 **See detailed guide**: [DEVELOPMENT_SETUP.md](DEVELOPMENT_SETUP.md)

**Quick setup:**
```bash
# Generate native platforms
cd client
npx cap add android
npx cap add ios

# Apply network configurations  
./scripts/setup-mobile-config.sh

# Sync and open in IDE
npx cap sync
npx cap open android  # or: npx cap open ios
```

> **Note**: Mobile platforms (android/ios folders) are excluded from Git and auto-generated. The setup script automatically configures network permissions for development.

## Technology Stack

### Frontend (Client)
- **Framework**: Ionic 7 + Angular 17
- **UI Components**: Ionic Components
- **HTTP Client**: Angular HttpClient
- **Styling**: SCSS + Ionic CSS Variables

### Backend (Server)
- **Framework**: .NET 8 Web API
- **ORM**: Entity Framework Core
- **Database**: In-Memory Database (for development)
- **API Documentation**: Swagger/OpenAPI

## API Endpoints

- `GET /api/todos` - Get all Todo items
- `GET /api/todos/{id}` - Get specific Todo item
- `POST /api/todos` - Create new Todo item
- `PUT /api/todos/{id}` - Update Todo item
- `DELETE /api/todos/{id}` - Delete Todo item

## Development Guide

### Frontend Development
- Use `ionic serve` in the `client` directory to start the development server
- Modify `client/src/environments/environment.ts` to configure API URL
- UI components are located in `client/src/app/pages/todo/`
- Ensure Ionic CLI is installed globally: `npm install -g @ionic/cli`

### Backend Development
- Use `dotnet run` to start the development server
- Swagger UI is available at `https://localhost:7001/swagger`
- Uses In-Memory Database, data will be reset after restart

## Deployment

### Frontend Deployment
```bash
# Navigate to frontend directory
cd client

# Install dependencies (if not already installed)
npm install

# Build production version
ionic build --prod
```

### Backend Deployment
```bash
cd server
dotnet publish -c Release
```

## Contributing

Pull Requests and Issues are welcome!
