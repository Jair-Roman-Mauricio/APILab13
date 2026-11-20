# APILab13 – Code First Semana 13

API REST en **ASP.NET Core Web API (.NET 10)** con **Entity Framework Core (Code First)** y **SQL Server 2022 en Docker**, desarrollada en macOS con Visual Studio Code y la línea de comandos.

## Modelo
`Grade (1) ── (N) Student (1) ── (N) Enrollment (N) ── (1) Course`

## Requisitos
- .NET SDK 10, `dotnet-ef` (`dotnet tool install -g dotnet-ef`)
- Docker Desktop

## Puesta en marcha
```bash
# 1. SQL Server en Docker
docker run -d --name lab13-sqlserver -e ACCEPT_EULA=Y \
  -e 'MSSQL_SA_PASSWORD=Lab13#Tecsup2026' -p 1433:1433 \
  mcr.microsoft.com/mssql/server:2022-latest

# 2. Migraciones
cd APILab13
dotnet ef database update

# 3. Ejecutar
dotnet run --launch-profile http
# Swagger: http://localhost:5057/swagger
```
Datos de prueba: `Scripts/seed.sql` (después de crear grados y cursos).

> En Windows con SQL Server local se puede usar la cadena `WindowsConnection` (autenticación de Windows, `Integrated Security=True`). En macOS/Docker la autenticación integrada de Windows no está disponible, por eso `DefaultConnection` usa autenticación SQL.

## Endpoints
| Método | Ruta | Descripción |
|---|---|---|
| GET/POST | `/api/Grades` | Listar activos / crear grado |
| GET/PUT/DELETE | `/api/Grades/{id}` | Obtener / actualizar / **eliminación lógica** |
| GET/POST | `/api/Courses` | Listar activos / crear curso |
| GET/PUT/DELETE | `/api/Courses/{id}` | Obtener / actualizar / **eliminación lógica** |
| GET | `/api/StudentsCustom/GetByNameLastNameEmail` | Por nombre, apellido y email, desc. por apellido |
| GET | `/api/StudentsCustom/GetByNameAndGrade` | Por nombre y grado, con datos del grado |
| GET | `/api/StudentsCustom/GetEnrolledByCourse` | Matriculados por curso, orden curso → apellido |
| GET | `/api/StudentsCustom/GetEnrolledByGrade` | Matriculados por grado, orden curso → apellido |
