# ⚽ Sistema de Reservas de Canchas de Fútbol (Backend MVP)

Este es el backend de una plataforma de gestión y reserva de canchas de fútbol, construido con **.NET 8** siguiendo los principios de **Clean Architecture** (Arquitectura Limpia).

## 🚀 Tecnologías Utilizadas
- **.NET 8 Web API**
- **Entity Framework Core** (Code-First con SQL Server)
- **JWT (JSON Web Tokens)** para Autenticación y Autorización por Roles (`User` y `Admin`)
- **BCrypt.Net** para el hashing seguro de contraseñas
- **FluentValidation** para la validación automática de datos de entrada
- **Middleware Global** para el manejo centralizado de excepciones

## 🛠️ Estructura del Proyecto
El proyecto está dividido en 4 capas según Clean Architecture:
1. `FootballReservation.Domain`: Entidades puras de negocio (`User`, `Field`, `Reservation`) y excepciones de dominio.
2. `FootballReservation.Application`: Interfaces, Servicios de aplicación, DTOs y Validadores.
3. `FootballReservation.Infrastructure`: Implementación de la Base de Datos (`AppDbContext`), Repositorios y Migraciones.
4. `FootballReservation.Api`: Controladores, Middlewares de error, Configuración de Autenticación y Swagger.

## 📌 Endpoints Clave de la API (Para la Integración del Frontend)

Todos los endpoints devuelven respuestas en formato JSON con nomenclatura `camelCase`.

### 🔐 Autenticación (Público)
- `POST /api/auth/register`: Registra un nuevo usuario. Requiere `firstName`, `lastName`, `email`, `password`.
- `POST /api/auth/login`: Autentica al usuario. Devuelve un objeto con los datos del usuario y un `token` JWT.

### 🏟️ Canchas (Requiere Token)
- `GET /api/fields`: Lista todas las canchas activas (Accesible por `User` y `Admin`).
- `POST /api/fields`: Crea una nueva cancha (Restringido solo a `Admin`). Requiere `name`, `capacity`, `pricePerHour`.

### 🗓️ Reservas (Requiere Token)
- `POST /api/reservations`: Crea una reserva de 1 a 4 horas. El sistema valida automáticamente que no haya colisiones de horarios. Requiere `fieldId`, `reservationDate` (UTC formato ISO) y `durationInHours`.

## ⚙️ Cómo Ejecutar el Backend
1. Clonar el repositorio.
2. Configurar la cadena de conexión a SQL Server en `appsettings.json`.
3. Ejecutar las migraciones: `dotnet ef database update --project FootballReservation.Infrastructure --startup-project FootballReservation.Api`
4. Iniciar la API: `dotnet run --project FootballReservation.Api`
5. Abrir el navegador en `http://localhost:XXXX/swagger` para ver la documentación interactiva.