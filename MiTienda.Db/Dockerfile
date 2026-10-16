# SQL Server para Railway (Root Directory del servicio: /MiTienda.Db)
# Imagen sobre Ubuntu 20.04: la de 22.04 falla con el almacenamiento de Railway
FROM mcr.microsoft.com/mssql/server:2022-CU23-ubuntu-20.04

# sudo solo para ajustar permisos del volumen. SQL Server NO debe arrancar como root:
# al cambiar de usuario pierde acceso a su propia memoria y se cae con "Stack Overflow"
USER root
RUN apt-get update \
    && apt-get install -y --no-install-recommends sudo \
    && rm -rf /var/lib/apt/lists/* \
    && echo "mssql ALL=(root) NOPASSWD: /usr/bin/mkdir, /usr/bin/chown" > /etc/sudoers.d/mssql \
    && chmod 0440 /etc/sudoers.d/mssql
COPY entrypoint.sh /usr/local/bin/entrypoint.sh
RUN chmod +x /usr/local/bin/entrypoint.sh

# Express: gratis para cualquier uso (maximo 10 GB por base)
ENV ACCEPT_EULA=Y \
    MSSQL_PID=Express \
    MSSQL_MEMORY_LIMIT_MB=2048 \
    MSSQL_MAX_SERVER_MEMORY_MB=512

USER 10001
EXPOSE 1433
ENTRYPOINT ["/usr/local/bin/entrypoint.sh"]
