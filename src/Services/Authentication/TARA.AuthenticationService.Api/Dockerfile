# Unter https://aka.ms/customizecontainer erfahren Sie, wie Sie Ihren Debugcontainer anpassen und wie Visual Studio dieses Dockerfile verwendet, um Ihre Images für ein schnelleres Debuggen zu erstellen.

# Diese Stufe wird verwendet, wenn sie von VS im Schnellmodus ausgeführt wird (Standardeinstellung für Debugkonfiguration).
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Diese Stufe wird zum Erstellen des Dienstprojekts verwendet.
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Authentication/TARA.AuthenticationService.Api/TARA.AuthenticationService.Api.csproj", "src/Services/Authentication/TARA.AuthenticationService.Api/"]
COPY ["src/Services/Authentication/TARA.AuthenticationService.Application/TARA.AuthenticationService.Application.csproj", "src/Services/Authentication/TARA.AuthenticationService.Application/"]
COPY ["src/Services/Authentication/TARA.AuthenticationService.Domain/TARA.AuthenticationService.Domain.csproj", "src/Services/Authentication/TARA.AuthenticationService.Domain/"]
COPY ["src/Services/Authentication/TARA.AuthenticationService.Infrastructure/TARA.AuthenticationService.Infrastructure.csproj", "src/Services/Authentication/TARA.AuthenticationService.Infrastructure/"]
RUN dotnet restore "./src/Services/Authentication/TARA.AuthenticationService.Api/TARA.AuthenticationService.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Authentication/TARA.AuthenticationService.Api"
RUN dotnet build "./TARA.AuthenticationService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Diese Stufe wird verwendet, um das Dienstprojekt zu veröffentlichen, das in die letzte Phase kopiert werden soll.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TARA.AuthenticationService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Diese Stufe wird in der Produktion oder bei Ausführung von VS im regulären Modus verwendet (Standard, wenn die Debugkonfiguration nicht verwendet wird).
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TARA.AuthenticationService.Api.dll"]