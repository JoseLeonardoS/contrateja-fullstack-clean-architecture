# ContrateJa API

A freelancer hiring platform built with .NET 10, Domain-Driven Design, and Clean Architecture.

---

## 🚀 Technologies

- **.NET 10** — Web API
- **Entity Framework Core** — ORM
- **PostgreSQL** — Database
- **MediatR** — CQRS pattern
- **FluentValidation** — Input validation
- **BCrypt** — Password hashing
- **JWT** — Authentication
- **Scalar** — API documentation
- **Docker** — Containerization
- **xUnit** — Unit testing

---

## 🏗️ Architecture

The project follows **Clean Architecture** with **Domain-Driven Design**:

```
ContrateJa.Domain         → Entities, Value Objects, Exceptions
ContrateJa.Application    → Use Cases, CQRS Handlers, Interfaces
ContrateJa.Infrastructure → Repositories, DbContext, Services
ContrateJa.API            → Controllers, Middlewares
ContrateJa.Tests          → Unit Tests
```

---

## 📦 Domain Overview

| Entity | Description |
|---|---|
| `User` | Platform user — can be a Freelancer, Contractor, or both |
| `Job` | Job posting created by a Contractor |
| `Proposal` | Proposal submitted by a Freelancer for a Job |
| `CompletedJob` | Record of a successfully completed Job |
| `Review` | Rating and comment left after a CompletedJob |
| `FreelancerArea` | Geographic area where a Freelancer is available |

---

## ⚙️ Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) — for running with Docker Compose
- [PostgreSQL](https://www.postgresql.org/) — for running locally without Docker

### Configuration

Create `appsettings.json` in `ContrateJa.API/` based on the example below.
> ⚠️ This file is listed in `.gitignore` and must be created manually.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=contrateja;Username=postgres;Password=yourpassword"
  },
  "Jwt": {
    "Secret": "your-secret-key-minimum-32-characters",
    "ExpiresInMinutes": "60"
  }
}
```

---

### Running with Docker Compose

1. Clone the repository:
```bash
git clone https://github.com/JoseLeonardoS/contrateja-fullstack-clean-architecture.git
cd contrateja-fullstack-clean-architecture
```

2. Create `appsettings.json` as shown above.

3. Start the containers:
```bash
docker-compose up --build
```

4. Apply migrations (in a separate terminal):
```bash
dotnet ef database update --project ContrateJa.Infrastructure --startup-project ContrateJa.API
```

5. Access the API documentation:
```
http://localhost:8080/scalar/v1
```

---

### Running Locally

1. Clone and restore:
```bash
git clone https://github.com/JoseLeonardoS/contrateja-fullstack-clean-architecture.git
cd contrateja-fullstack-clean-architecture
dotnet restore
```

2. Make sure PostgreSQL is running locally and create `appsettings.json` as shown above.

3. Apply migrations:
```bash
dotnet ef database update --project ContrateJa.Infrastructure --startup-project ContrateJa.API
```

4. Run the API:
```bash
dotnet run --project ContrateJa.API
```

5. Access the API documentation:
```
https://localhost:7280/scalar/v1
```

---

## 📚 API Resources

| Resource | Description |
|---|---|
| `/api/users` | User management |
| `/api/jobs` | Job postings |
| `/api/proposals` | Freelancer proposals |
| `/api/reviews` | Reviews and ratings |
| `/api/completedJobs` | Completed jobs |
| `/api/freelancerAreas` | Freelancer service areas |

Full documentation with all endpoints and request/response schemas available at `/scalar/v1` after running the project.

---

## 🧪 Tests

```bash
dotnet test
```

Unit tests cover all domain entities and value objects.

---

## 📄 License

MIT