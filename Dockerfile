FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/People.API/People.API.csproj", "src/People.API/"]
COPY ["src/People.Infrastructure/People.Infrastructure.csproj", "src/People.Infrastructure/"]
COPY ["src/People.Application/People.Application.csproj", "src/People.Application/"]
COPY ["src/People.Domain/People.Domain.csproj", "src/People.Domain/"]
RUN dotnet restore "src/People.API/People.API.csproj"
COPY . .
WORKDIR "/src/src/People.API"
RUN dotnet build "People.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "People.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "People.API.dll"]
