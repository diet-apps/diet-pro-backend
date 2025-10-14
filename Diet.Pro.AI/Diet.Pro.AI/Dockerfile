# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Definir diretório de trabalho para o build
WORKDIR /src

# Copiar o csproj para restaurar dependências primeiro (cache do Docker)
COPY Diet.Pro.AI/Diet.Pro.AI/Diet.Pro.AI.csproj Diet.Pro.AI/Diet.Pro.AI/

# Restaurar dependências
RUN dotnet restore "Diet.Pro.AI/Diet.Pro.AI/Diet.Pro.AI.csproj"

# Copiar o restante do código fonte
COPY . .

# Criar diretório de saída do build e garantir permissões
RUN mkdir -p /app/build && chmod -R 777 /app/build

# Definir diretório de trabalho para o projeto específico
WORKDIR /src/Diet.Pro.AI

# Buildar o projeto, output para /app/build
RUN dotnet build "Diet.Pro.AI.csproj" -c Release -o /app/build

# Etapa 2: Publish (opcional, caso use publish)
FROM build AS publish
RUN dotnet publish "Diet.Pro.AI.csproj" -c Release -o /app/publish

# Etapa 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app

# Copiar os arquivos publicados para o container runtime
COPY --from=publish /app/publish .

# Expor porta (se aplicável)
EXPOSE 80

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "Diet.Pro.AI.dll"]
