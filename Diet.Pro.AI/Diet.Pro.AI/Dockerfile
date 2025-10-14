# Base image para execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Fase de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia o arquivo .csproj para restaurar dependências
COPY ["Diet.Pro.AI/Diet.Pro.AI/Diet.Pro.AI.csproj", "Diet.Pro.AI/"]
RUN dotnet restore "Diet.Pro.AI/Diet.Pro.AI.csproj"

# Copia todo o restante do código
COPY . .

# Define o diretório do projeto
WORKDIR "/src/Diet.Pro.AI"

# Build do projeto com saída para /build (não /app/build)
RUN dotnet build "Diet.Pro.AI.csproj" -c $BUILD_CONFIGURATION -o /build

# Publicação do projeto para /publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Diet.Pro.AI.csproj" -c $BUILD_CONFIGURATION -o /publish /p:UseAppHost=false

# Imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /publish .
ENTRYPOINT ["dotnet", "Diet.Pro.AI.dll"]
