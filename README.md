# TODO-APP

## Overview

This project is a web application with a backend built using ASP.NET and a frontend developed with Vue 3 and TypeScript.

## Project Structure

```
project-root/
│── backend/         # ASP.NET backend
│   ├── Controllers/  # API controllers
│   ├── Models/       # Database models
│   ├── Services/     # Business logic services
│   ├── Repositories/ # Data access layer
│   ├── Config/       # Configuration files
│   ├── appsettings.json # Application settings
│   ├── Program.cs    # Entry point
│   ├── Startup.cs    # Middleware and services
│
│── frontend/        # Vue 3 frontend with TypeScript
│   ├── src/
│   │   ├── components/  # Reusable UI components
│   │   ├── views/       # Page views
│   │   ├── store/       # Vuex/Pinia state management
│   │   ├── router/      # Vue Router configuration
│   │   ├── utils/       # Utility functions
│   │   ├── App.vue      # Root Vue component
│   │   ├── main.ts      # Entry point
│   ├── public/         # Static assets
│   ├── package.json    # Dependencies and scripts
│   ├── tsconfig.json   # TypeScript configuration
│   ├── vite.config.ts  # Vite configuration
```

## Backend (ASP.NET)

- **Framework**: ASP.NET Core
- **Database**: SQL Server
- **Authentication**: JWT-based authentication
- **ORM**: Dapper
- **Environment Variables**: Managed in `appsettings.json`

### Running the Backend

```sh
cd backend
# Run the application
dotnet restore
dotnet run
```

### Modify appsettings.json

```
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=YourDatabase;User Id=yourUser;Password=yourPassword;"
}
```

## Frontend (Vue 3 + TypeScript)

- **Framework**: Vue 3
- **State Management**: Pinia
- **Routing**: Vue Router
- **UI Library**: TailwindCSS
- **Build Tool**: Vite
- **Linting**: ESLint & Prettier

### Running the Frontend

```sh
cd frontend
# Install dependencies
npm install
# Start the development server
npm run dev
```

### Port config

```
matching the port in frontend part to be the same as backend port
eg. backend port: http://localhost:5000/api/auth/register
search for port 5068 and change to 5000.
```

## License

This project is licensed under the MIT License.
