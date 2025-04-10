# Trubaduren - Backend API

Detta är backendprojektet för Trubaduren – en webbtjänst som hanterar låtar, önskemål och andra funktioner för en trubadur. Projektet är byggt med ASP.NET Core och använder Entity Framework Core tillsammans med SQLite som databas. Eventuella integreringar med frontend-ramverk tillkommer och annan databas ska senare införas.

## 🧰 Teknikstack

- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Mapster (för objektmappning)

---

## 🚀 Kom igång

Följ stegen nedan för att komma igång med projektet lokalt.

### 1. Klona repot

```bash
git clone https://github.com/robinLundahl/webapi.git
cd webapi

Om du använder .NET SDK 7.0 eller senare, kör:
dotnet restore

Om du använder Entity Framework migrations (Se till att du har dotnet-ef installerat globalt: dotnet tool install --global dotnet-ef):
dotnet ef database update

Kör projektet:
dotnet run

```

API:t är nu tillgängligt på http://localhost:5240/Swagger/index.html
