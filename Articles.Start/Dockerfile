FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Articles.Start/Articles.Start.csproj", "Articles.Start/"]
COPY ["Articles.Infrastructure.Web/Articles.Infrastructure.Web.csproj", "Articles.Infrastructure.Web/"]
COPY ["Articles.Application.Abstractions/Articles.Application.Abstractions.csproj", "Articles.Application.Abstractions/"]
COPY ["Articles.Domain/Articles.Domain.csproj", "Articles.Domain/"]
COPY ["Articles.Infrastructure.Storage/Articles.Infrastructure.Storage.csproj", "Articles.Infrastructure.Storage/"]
COPY ["Articles.Application.Services/Articles.Application.Services.csproj", "Articles.Application.Services/"]
RUN dotnet restore "Articles.Start/Articles.Start.csproj"
COPY . .
WORKDIR "/src/Articles.Start"
RUN dotnet build "./Articles.Start.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Articles.Start.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Articles.Start.dll"]
