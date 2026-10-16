# Imagen de la Web (Blazor WebAssembly) para Railway (Root Directory del servicio: /MiTienda.Web)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY MiTienda.Web.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app --no-restore

# Blazor WebAssembly son archivos estaticos: los sirve nginx
FROM nginx:1.27-alpine
ENV PORT=8080
COPY nginx.conf.template /etc/nginx/templates/default.conf.template
COPY api-url.sh /docker-entrypoint.d/40-api-url.sh
RUN chmod +x /docker-entrypoint.d/40-api-url.sh
COPY --from=build /app/wwwroot /usr/share/nginx/html
