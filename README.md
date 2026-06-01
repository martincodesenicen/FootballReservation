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
