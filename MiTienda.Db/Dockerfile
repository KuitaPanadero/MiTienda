# SQL Server para Railway (Root Directory del servicio: /MiTienda.Db)
# Imagen sobre Ubuntu 20.04: la de 22.04 falla con el almacenamiento de Railway
FROM mcr.microsoft.com/mssql/server:2022-CU23-ubuntu-20.04

# Express: gratis para cualquier uso (maximo 10 GB por base y ~1.4 GB de cache)
ENV ACCEPT_EULA=Y \
    MSSQL_PID=Express \
    MSSQL_MEMORY_LIMIT_MB=2048

# Corre como root para poder escribir en el volumen de Railway montado en /var/opt/mssql
USER root

EXPOSE 1433
