# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# ✅ Copia o .csproj com caminho relativo à raiz do repositório
COPY ["Diet.Pro.AI/Diet.Pro.AI/Diet.Pro.AI.csproj", "Diet.Pro.AI/"]
RUN dotnet restore "Diet.Pro.AI/Diet.Pro.AI.csproj"

# ✅ Copia tudo
COPY . .

WORKDIR "/src/Diet.Pro.AI"
RUN dotnet build "Diet.Pro.AI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Diet.Pro.AI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Diet.Pro.AI.dll"]
