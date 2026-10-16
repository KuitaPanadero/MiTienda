#!/bin/bash
# Arranque de SQL Server en Railway

# Un limite de archivos abiertos enorme hace que sudo tarde minutos en arrancar
ulimit -n 65536 2>/dev/null

# Railway monta el volumen como root: se le pasa al usuario mssql (10001)
echo "[entrypoint] ajustando permisos del volumen"
timeout 60 sudo -n /usr/bin/mkdir -p /var/opt/mssql || echo "[entrypoint] mkdir fallo"
timeout 120 sudo -n /usr/bin/chown -R 10001:0 /var/opt/mssql || echo "[entrypoint] chown fallo"
ls -ld /var/opt/mssql

# Borra volcados de memoria de caidas anteriores (llenan el volumen)
rm -rf /var/opt/mssql/log/core.*

# Si la base nunca termino de inicializarse (caida a medias), arranca limpia
if [ ! -f /var/opt/mssql/.inicializado ]; then
    echo "[entrypoint] volumen sin inicializar: limpiando restos"
    rm -rf /var/opt/mssql/data /var/opt/mssql/log /var/opt/mssql/secrets /var/opt/mssql/.system
fi

# El disco de Railway usa sectores de 4K: sin el trace flag 1800 SQL Server cae con "Stack Overflow"
if ! grep -q "traceflag0 = 1800" /var/opt/mssql/mssql.conf 2>/dev/null; then
    printf '[traceflag]\ntraceflag0 = 1800\n' >> /var/opt/mssql/mssql.conf
fi

echo "[entrypoint] iniciando SQL Server"
/opt/mssql/bin/sqlservr &
pid=$!
trap 'kill -TERM $pid' TERM INT

# Cuando SQL Server responda, limita su cache para no pasar la RAM del plan
sqlcmd=$(ls /opt/mssql-tools*/bin/sqlcmd 2>/dev/null | tail -1)
for i in $(seq 1 60); do
    if "$sqlcmd" -S localhost -U sa -P "$MSSQL_SA_PASSWORD" -C -b -Q \
        "EXEC sp_configure 'show advanced options', 1; RECONFIGURE; EXEC sp_configure 'max server memory (MB)', $MSSQL_MAX_SERVER_MEMORY_MB; RECONFIGURE;" >/dev/null 2>&1; then
        touch /var/opt/mssql/.inicializado
        echo "SQL Server listo (max server memory = $MSSQL_MAX_SERVER_MEMORY_MB MB)"
        break
    fi
    sleep 2
done

wait $pid
