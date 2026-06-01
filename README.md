Estaba haciendo mi README.md y en la ultima parte como que se me desformatearon las cosas. Podrías formatearlas? No agregues ni cambies informacion, solo ponles el formato que corresponda

# ⚽ Sistema de Reservas de Canchas de Fútbol (Fullstack MVP)

Este es un ecosistema completo para la gestión y reserva de canchas de fútbol. Está compuesto por un backend robusto en **.NET 8** diseñado bajo los principios de **Clean Architecture** y una aplicación web moderna y reactiva en **Angular**.

---

## 🚀 Tecnologías Utilizadas

### 🨔 Backend (.NET 8 Web API)
- **Entity Framework Core** (Code-First con SQL Server)
- **JWT (JSON Web Tokens)** para Autenticación y Autorización por Roles (`Client` y `Admin`)
- **BCrypt.Net** para el hashing seguro de contraseñas
- **FluentValidation** para la validación automática de datos de entrada
- **Middleware Global** para el manejo centralizado de excepciones y errores de negocio

### 🅰 Frontend (Angular SPA)
- **Angular (Última Versión)** basada al 100% en **Standalone Components** (sin NgModules)
- **Nuevo flujo de control de Angular** (`@if`, `@for`) para una renderización de UI ultra eficiente
- **Formularios Reactivos** con validaciones en el cliente
- **Manejo funcional de Interceptores y Guards** para la inyección automática del JWT y protección de rutas por roles

---

## 🛠️ Estructura del Proyecto

El espacio de trabajo se organiza de forma clara dividiendo las responsabilidades del servidor y del cliente:

- **FootballReservation.sln** -> Solución global de .NET
- **src/** -> Carpeta del Backend
  - `FootballReservation.Domain`: Entidades puras de negocio (User, Field, Reservation)
  - `FootballReservation.Application`: Interfaces, Servicios, DTOs y Validadores
  - `FootballReservation.Infrastructure`: Base de Datos (DbContext), Repositorios y Migraciones
  - `FootballReservation.Api`: Controladores, Interfaz Swagger y Middlewares
- **football-reservation-web/** -> Carpeta del Frontend (Angular)
  - `src/app/core/`: Elementos globales (Servicios, Interceptores, Guards, Modelos)
  - `src/app/features/`: Módulos de página (Auth, Customer-Dashboard, Admin-Dashboard)

---

## 📌 Arquitectura del Frontend e Integración

La SPA de Angular está estructurada siguiendo las mejores prácticas para asegurar un acoplamiento limpio con los endpoints de .NET:

1. **Autenticación en Cadena (Core/Services/AuthService):** El proceso de login/registro envía las credenciales a la API, almacena el JWT de forma segura en el LocalStorage y realiza un encadenamiento automático (switchMap) hacia el endpoint de perfil (/api/users/me) para determinar el rol del usuario en tiempo real.
2. **Interceptor Funcional (Core/Interceptors/Jwt):** Clona cada petición saliente dirigida a la API e inyecta dinámicamente la cabecera `Authorization: Bearer <token>`, evitando adjuntar el token de forma manual en cada servicio.
3. **Guardia Funcional por Roles (Core/Guards/Auth):** Protege las rutas del sistema del lado del cliente. Si un usuario con rol Client intenta ingresar por URL al panel de administración, la guardia intercepta la navegación y lo redirige automáticamente a la pantalla de autenticación.

---

## 💻 Pantallas Principales del Frontend

- **🔐 Login / Registro Integrado:** Un componente unificado y dinámico con formularios reactivos que valida el formato de correos y la extensión de contraseñas antes de interactuar con el servidor. Redirige inteligentemente según el rol devuelto por el perfil.
- **🏟️ Dashboard del Administrador (/admin-dashboard):** Vista protegida para cuentas con rol Admin. Dispone de un formulario reactivo controlado para el alta de canchas enlazado directamente a los DTOs de C#.
- **🗓️ Dashboard del Cliente (/customer-dashboard):** Vista protegida para cuentas con rol Client. Muestra de manera reactiva el catálogo de canchas activas, cuenta con un formulario interactivo con selector de fecha/hora para agendar turnos (de 1 a 4 horas) y despliega un historial en tiempo real con las reservas del usuario conectado.

---

## ⚙️ Cómo Ejecutar el Ecosistema Completo

### 1. Levantar el Backend

1. Configurar la cadena de conexión a tu instancia de SQL Server dentro de `src/FootballReservation.Api/appsettings.json`.

2. Posicionarte en la raíz y aplicar las migraciones para generar la base de datos:

   ```bash
   dotnet ef database update --project src/FootballReservation.Infrastructure --startup-project src/FootballReservation.Api
   ```

3. Iniciar la API (correrá por defecto en el puerto configurado, ej: http://localhost:5035):

   ```bash
   dotnet run --project src/FootballReservation.Api
   ```

### 2. Levantar el Frontend

Abrir una nueva terminal e ingresar a la carpeta del cliente:

```bash
cd football-reservation-web
```

Instalar las dependencias necesarias de Angular:

```bash
npm install
```

Iniciar el servidor de desarrollo local:

```bash
ng serve
```

Abrir el navegador en `http://localhost:4200` para empezar a operar la aplicación.

---

## 🛡️ Notas de Seguridad para el Entorno de Desarrollo

### Asignación del Rol Admin

Para mantener el formulario de registro público seguro, por defecto los nuevos usuarios nacen con el rol Client. Para interactuar como administrador, regístrate normalmente y modifica la columna Role directamente en tu tabla de Base de Datos al valor exacto de Admin.

### CORS

El backend cuenta con una política de CORS activa para permitir de manera exclusiva las peticiones asíncronas procedentes del dominio del cliente (`http://localhost:4200`).
