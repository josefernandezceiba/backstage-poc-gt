### Project Overview

This is a C# .NET 10 web application named "MoMo". It is built using a modular monolith architecture, where different functionalities are separated into modules. The project uses a layered architecture within each module, and it follows the Ports and Adapters (Hexagonal Architecture) pattern. The communication between modules is event-driven, using RabbitMQ as a message broker.

**Key Technologies:**

*   .NET 10
*   ASP.NET Core
*   Entity Framework Core
*   SQL Server
*   RabbitMQ
*   Docker
*   Serilog
*   OpenAPI (Swagger) with Scalar

**Modules:**

*   **Onboarding:** Handles user registration.
*   **Notifications:** Sends notifications, like welcome emails.

### Building and Running

**Build:**

```bash
dotnet build ./Momo.csproj --configuration Release
```

**Run:**

To run the application, you can use the `dotnet run` command. You will also need to have SQL Server and RabbitMQ instances running.

```bash
# To run the complete application (all modules)
dotnet run

# To run a specific module (e.g., Onboarding)
CURRENT_MODULE=ONBOARDING dotnet run
```

**Docker:**

The project includes a `Dockerfile` for containerization.

```bash
# Build the Docker image
docker build -t momo .

# Run the Docker container
docker run -p 8080:8080 -e DBHOST=<your_db_host> -e DBPORT=<your_db_port> momo
```

### Development Conventions

*   **Modular Architecture:** The project is divided into modules. When adding new functionality, consider creating a new module or adding to an existing one.
*   **Layered Architecture:** Follow the layered architecture within each module (`Core`, `Features`, `Infrastructure`).
*   **Ports and Adapters:** Use interfaces (ports) to define contracts and adapters for concrete implementations.
*   **Event-Driven:** Use events for communication between modules.
*   **API Endpoints:** API endpoints are defined in the `Features` layer of each module. The `ApiEndpoint` attribute is used to group endpoints by module.
*   **Logging:** The project uses Serilog for structured logging.
*   **CI/CD:** A GitHub Actions workflow is set up for continuous integration. The workflow builds the project and a Docker image on every push to the `main` branch.
