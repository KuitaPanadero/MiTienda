# Imagen de la Api para Railway (Root Directory del servicio: /MiTienda.Api)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY MiTienda.Api.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
# Railway indica el puerto en la variable PORT
CMD ASPNETCORE_HTTP_PORTS=${PORT:-8080} dotnet MiTienda.Api.dll
