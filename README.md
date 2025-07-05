# Encuestas UDEP

Ejemplo de implementación monolítica en .NET 9 usando Dapper y procedimientos almacenados.

## Estructura de la solución

- **DataAccess**: Capa de acceso a datos mediante Dapper.
- **Business**: Lógica de negocio.
- **UDEP.Encuestas.WebAPI**: API RESTful protegida con JWT.
- **UDEP.Encuestas.Web**: Aplicación web que consume la API y maneja el login.

Se incluyen entidades, repositorios, servicios y controladores para todas las tablas definidas en `DBENCUESTAS_UDEP.sql` utilizando un repositorio genérico basado en Dapper.

## Configuración

1. Actualizar la cadena de conexión y la clave JWT en `appsettings.json`.
2. Ejecutar la base de datos usando el script `DBENCUESTAS_UDEP.sql` y asegurar que los procedimientos almacenados estén disponibles.

## Uso

Con la API en ejecución puede acceder a `/swagger` para probar los endpoints. Ingrese un `Bearer` token obtenido desde el endpoint `/api/auth/login`.
La aplicación web realiza un login básico y almacena el token en sesión para consumir la API.

## Desarrollo

Todas las operaciones de I/O son asíncronas y se implementa un pool de conexiones mediante `SqlConnection`.

El caching en memoria está disponible a través de `IMemoryCache` y se puede utilizar en los servicios de negocio para optimizar consultas frecuentes.

