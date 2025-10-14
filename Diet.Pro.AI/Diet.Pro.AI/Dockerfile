# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copiar o arquivo .csproj
COPY Diet.Pro.AI/Diet.Pro.AI/Diet.Pro.AI.csproj Diet.Pro.AI/Diet.Pro.AI/

# Restaurar dependências
RUN dotnet restore "Diet.Pro.AI/Diet.Pro.AI/Diet.Pro.AI.csproj"

# Copiar o restante do código
COPY . .

RUN mkdir -p /app/build && chmod -R 777 /app/build

# Ajuste aqui: definir WORKDIR correto para o build
WORKDIR /src/Diet.Pro.AI/Diet.Pro.AI

RUN dotnet build "Diet.Pro.AI.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "Diet.Pro.AI.csproj" -c Release -o /app/publish

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app

COPY --from=publish /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "Diet.Pro.AI.dll"]
