#!/bin/bash
# Arranque de SQL Server en Railway

# Railway monta el volumen como root: se le pasa al usuario mssql (10001)
sudo /usr/bin/mkdir -p /var/opt/mssql
sudo /usr/bin/chown -R 10001:0 /var/opt/mssql

# Borra volcados de memoria de caidas anteriores (llenan el volumen)
rm -rf /var/opt/mssql/log/core.*

/opt/mssql/bin/sqlservr &
pid=$!
trap 'kill -TERM $pid' TERM INT

# Cuando SQL Server responda, limita su cache para no pasar la RAM del plan
sqlcmd=$(ls /opt/mssql-tools*/bin/sqlcmd 2>/dev/null | tail -1)
for i in $(seq 1 60); do
    if "$sqlcmd" -S localhost -U sa -P "$MSSQL_SA_PASSWORD" -C -b -Q \
        "EXEC sp_configure 'show advanced options', 1; RECONFIGURE; EXEC sp_configure 'max server memory (MB)', $MSSQL_MAX_SERVER_MEMORY_MB; RECONFIGURE;" >/dev/null 2>&1; then
        echo "SQL Server listo (max server memory = $MSSQL_MAX_SERVER_MEMORY_MB MB)"
        break
    fi
    sleep 2
done

wait $pid
