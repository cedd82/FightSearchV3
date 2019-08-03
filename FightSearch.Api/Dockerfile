FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 800
EXPOSE 4433

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["FightSearch.Api/FightSearch.Api.csproj", "FightSearch.Api/"]
COPY ["FightSearch.Service/FightSearch.Service.csproj", "FightSearch.Service/"]
COPY ["FightSearch.Common/FightSearch.Common.csproj", "FightSearch.Common/"]
COPY ["FightSearch.Repository.Sql/FightSearch.Repository.Sql.csproj", "FightSearch.Repository.Sql/"]
RUN dotnet restore "FightSearch.Api/FightSearch.Api.csproj"
COPY . .
WORKDIR "/src/FightSearch.Api"
RUN dotnet build "FightSearch.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FightSearch.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FightSearch.Api.dll"]
