# MiTienda

API REST en ASP.NET Core 10 con Entity Framework Core y SQL Server, y frontend en Blazor WebAssembly.

| Proyecto | Qué es |
|---|---|
| `MiTienda.Api` | API de Clientes, Proveedores y Métodos de pago (Swagger en `/swagger`) |
| `MiTienda.Web` | Frontend Blazor WebAssembly que consume la API |

## Ejecutar en local

1. Abrir `MiTienda.sln` en Visual Studio.
2. Iniciar `MiTienda.Api` y `MiTienda.Web` (perfil de inicio múltiple).
3. La base de datos `MiTiendaDb` se crea sola en LocalDB al arrancar la API (aplica las migraciones).

## Despliegue en Railway

Cada proyecto es un servicio con su propio `Dockerfile`.

| Servicio | Root Directory | Variable |
|---|---|---|
| SqlServer | `/MiTienda.Db` | `MSSQL_SA_PASSWORD` + volumen en `/var/opt/mssql` |
| API | `/MiTienda.Api` | `ConnectionStrings__Default` = cadena de conexión de SQL Server |
| Web | `/MiTienda.Web` | `API_URL` = dominio público de la API, terminado en `/` |

SQL Server necesita 2 GB de RAM, por eso requiere el plan Hobby de Railway (el Trial da 1 GB por servicio).
