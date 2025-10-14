# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Corrigido: nome correto da solução
COPY Diet.Pro.Al.sln ./
COPY Diet.Pro.Al/*.csproj ./Diet.Pro.Al/
RUN dotnet restore Diet.Pro.Al.sln

COPY . ./

RUN dotnet publish Diet.Pro.Al/Diet.Pro.Al.csproj -c Release -o /app/out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "Diet.Pro.Al.dll"]
