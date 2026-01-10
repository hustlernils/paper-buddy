# PaperBuddy Agent Guidelines

This document provides essential information for agentic coding assistants working in the PaperBuddy repository. It covers build/lint/test commands and code style guidelines to ensure consistent development practices.

## Build, Lint, and Test Commands

### Frontend (React/TypeScript)
Located in `src/frontend/`. Run commands from this directory.

#### Development
```bash
npm run dev  # Start development server with Vite
```

#### Build
```bash
npm run build  # Type-check and build for production (tsc -b && vite build)
```

#### Lint
```bash
npm run lint  # Run ESLint on all TypeScript/React files
```

#### Preview
```bash
npm run preview  # Preview production build locally
```

### Backend (.NET)
Located in `src/backend/`. Run commands from the solution root.

#### Build
```bash
dotnet build  # Build all projects in the solution
```

#### Test
```bash
dotnet test  # Run all tests in test projects (PaperBuddy.Web.Tests, PaperBuddy.MessageBus.Tests)
```

To run tests for a specific project:
```bash
dotnet test test/PaperBuddy.Web.Tests/  # Run web API tests only
dotnet test test/PaperBuddy.MessageBus.Tests/  # Run message bus tests only
```

To run a single test:
```bash
dotnet test --filter "TestName"  # Replace TestName with the specific test method name
```

#### Run
```bash
dotnet run --project src/backend/PaperBuddy.Web/  # Start the web API server
```

### Full Stack Development

#### Start all services (database, Ollama, etc.)
```bash
./start-stack.sh  # Starts database with Docker Compose and sets up Ollama
```

#### Database migrations
```bash
bash scripts/run-migration.sh  # Run database migration scripts
```

## Code Style Guidelines

### TypeScript/React Frontend

#### File Structure and Organization
- Components: `app/pages/` for page components, `app/components/` for reusable components
- Hooks: `app/hooks/` for custom React hooks
- Types: `app/types/` for TypeScript type definitions
- State management: Custom hooks + reducers pattern (`app/reducers/`)

#### Import Style
```typescript
// Group imports: React/React types, then third-party libraries, then local imports
import React, { type FormEvent, useState } from "react";
import { useNavigate } from "react-router-dom";

// Third-party UI components
import { Button } from "../components/ui/Button";
import { Label } from "../components/ui/Label";

// Local types and utilities
import { type GetProjectsResponse } from "../types/api";
import Grid from '../components/layout/Grid';

// Path mapping: Use @/* for app directory imports
import { useProjects } from "@/hooks/useProjects";
```

#### Function and Component Style
```typescript
// Arrow functions with Allman braces (ESLint enforced)
const Projects = () => 
{
  // Component logic here
  return (
    <div>
      {/* JSX content */}
    </div>
  );
};

// Hook functions
export const useProjects = (): UseProjectsResponse => 
{
  // Hook implementation
};
```

#### TypeScript Conventions
- **Strict mode enabled**: `noUnusedLocals`, `noUnusedParameters`, `strict: true`
- **Interface naming**: PascalCase with optional `I` prefix for complex types
- **Type imports**: Use `import { type TypeName }` syntax
- **Generic constraints**: Prefer specific types over `any`
- **Optional properties**: Use `?` for optional object properties
- **Union types**: Use discriminated unions for complex state

#### React Patterns
- **Functional components**: No class components
- **Custom hooks**: For shared logic and API calls
- **Event handlers**: Inline arrow functions for simple cases
- **Props**: Prefer destructuring in function parameters
- **State management**: Custom hooks + useReducer for complex state

#### Error Handling
```typescript
// API calls with error handling
const fetchProjects = async () => 
{
  try {
    const response = await api.get<GetProjectsResponse[]>('/projects');
    if (response) {
      setProjects(response);
    }
  } catch (error) {
    console.error('Failed to fetch projects:', error);
    // Handle error state appropriately
  }
};
```

#### Naming Conventions
- **Components**: PascalCase (`Projects`, `ProjectDetails`)
- **Hooks**: camelCase with `use` prefix (`useProjects`, `useFetch`)
- **Functions**: camelCase (`fetchProjects`, `handleProjectChange`)
- **Constants**: UPPER_SNAKE_CASE for configuration values
- **Files**: PascalCase for components, camelCase for utilities

### C# Backend

#### Project Structure
- **CQRS Pattern**: Separate handlers and endpoints for each feature
- **Domain**: `Domain/` for entities and business logic
- **Features**: `Features/FeatureName/` with Handler, Endpoint, Request, Response classes
- **Infrastructure**: `Infrastructure/` for cross-cutting concerns (DI, services)

#### Class and Method Style
```csharp
// Handler classes with primary constructor
public class GetProjectsHandler(IDbConnection connection)
{
    private readonly IDbConnection _dbConnection = connection;
    
    public async Task<IEnumerable<GetProjectsResponse>> HandleAsync(
        GetProjectsRequest request, 
        CancellationToken cancellationToken)
    {
        // Method implementation
    }
}
```

#### Naming Conventions
- **Classes**: PascalCase (`GetProjectsHandler`, `Project`)
- **Methods**: PascalCase (`HandleAsync`, `MapGetProjectsEndpoint`)
- **Properties**: PascalCase (`UserId`, `Title`)
- **Private fields**: camelCase with underscore prefix (`_dbConnection`)
- **Namespaces**: PascalCase matching directory structure
- **Files**: Match class name exactly

#### TypeScript Integration
- **Nullable enabled**: Use `?` for nullable reference types
- **Async/await**: Always use async/await pattern, never `.Result`
- **Dependency injection**: Constructor injection preferred
- **Cancellation tokens**: Accept `CancellationToken` in async methods

#### API Endpoints
```csharp
// Minimal API with proper status codes
public static class GetProjectsEndpoint
{
    public static void MapGetProjectsEndpoint(this WebApplication app)
    {
        app.MapGet("/projects", async (
                [FromServices] GetProjectsHandler handler) =>
            {
                var request = new GetProjectsRequest();
                var responses = await handler.HandleAsync(request, CancellationToken.None);
                return Results.Ok(responses);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .DisableAntiforgery();
    }
}
```

#### Error Handling
```csharp
// Try-catch with appropriate HTTP status codes
try {
    // Operation that might fail
} catch (Exception ex) {
    // Log error
    return Results.BadRequest("Operation failed");
}
```

#### Database Operations
- **Dapper**: Use parameterized queries to prevent SQL injection
- **Connection management**: Open/close connections explicitly
- **Async operations**: Use `QueryAsync`, `ExecuteAsync` for database operations

#### Entity Design
```csharp
public class Project : TrackedEntity
{
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }

    public Project(Guid userId, string title, string? description = null)
    {
        UserId = userId;
        Title = title;
        Description = description;
    }
}
```

### General Guidelines

#### Commit Messages
- Follow conventional commit format: `type(scope): description`
- Types: `feat`, `fix`, `docs`, `style`, `refactor`, `test`, `chore`
- Keep first line under 50 characters
- Use imperative mood ("Add feature" not "Added feature")

#### Testing
- **Backend**: xUnit with async test methods
- **Frontend**: No testing framework configured yet (consider Vitest)
- **Integration tests**: Test full request/response cycles
- **Unit tests**: Test handlers and services in isolation

#### Security
- **Input validation**: Validate all user inputs
- **SQL injection**: Use parameterized queries
- **Authentication**: Implement proper auth for protected endpoints
- **Secrets**: Never commit API keys, connection strings, or secrets

#### Performance
- **Database**: Use proper indexing and avoid N+1 queries
- **Async**: Use async/await throughout the application stack
- **Caching**: Implement caching for expensive operations
- **Lazy loading**: Load data on-demand when possible

Always run `npm run lint` (frontend) and `dotnet build` (backend) before committing changes. For complex features, create appropriate tests to verify functionality.</content>
<parameter name="filePath">/home/nils/paper-buddy/AGENTS.md