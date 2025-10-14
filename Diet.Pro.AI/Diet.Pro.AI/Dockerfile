# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copiar o arquivo csproj e restaurar dependências (cache eficiente)
COPY Diet.Pro.AI/Diet.Pro.AI.csproj Diet.Pro.AI/

RUN dotnet restore "Diet.Pro.AI/Diet.Pro.AI.csproj"

# Copiar o restante do código
COPY . .

# Diretório para saída do build
RUN mkdir -p /src/build_output && chmod -R 777 /src/build_output

WORKDIR /src/Diet.Pro.AI

# Buildar e enviar saída para build_output
RUN dotnet build "Diet.Pro.AI.csproj" -c Release -o /src/build_output

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "Diet.Pro.AI.csproj" -c Release -o /app/publish

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=publish /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "Diet.Pro.AI.dll"]
