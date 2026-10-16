#!/bin/sh
# Escribe la URL de la Api (variable API_URL del servicio en Railway) en appsettings.json
if [ -n "$API_URL" ]; then
    printf '{\n  "ApiUrl": "%s"\n}\n' "$API_URL" > /usr/share/nginx/html/appsettings.json
    rm -f /usr/share/nginx/html/appsettings.json.br /usr/share/nginx/html/appsettings.json.gz
fi
