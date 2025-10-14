# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia arquivos da solution e do projeto
COPY Diet.Pro.AI.sln ./
COPY Diet.Pro.AI/*.csproj ./Diet.Pro.AI/
RUN dotnet restore Diet.Pro.AI.sln

# Copia tudo
COPY . ./

# Publica o projeto
RUN dotnet publish Diet.Pro.AI/Diet.Pro.AI.csproj -c Release -o /app/out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "Diet.Pro.AI.dll"]
