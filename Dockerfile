FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
#EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["FightSearch.Api/FightSearch.Api.csproj", "FightSearch.Api/"]
COPY ["FightSearch.Service/FightSearch.Service.csproj", "FightSearch.Service/"]
COPY ["FightSearch.Common/FightSearch.Common.csproj", "FightSearch.Common/"]
COPY ["FightSearch.Repository.Sql/FightSearch.Repository.Sql.csproj", "FightSearch.Repository.Sql/"]
RUN dotnet restore "FightSearch.Api/FightSearch.Api.csproj"
COPY . .
WORKDIR /src/FightSearch.Api
RUN dotnet build "FightSearch.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FightSearch.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FightSearch.Api.dll"]



# For more info see: http://aka.ms/VSContainerToolingDockerfiles
#FROM microsoft/aspnetcore:2.2 AS base
#FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
#WORKDIR /app
#EXPOSE 80
#
#FROM microsoft/aspnetcore-build:2.2 AS builder
#WORKDIR /src
#COPY *.sln ./
#COPY ["FightSearch.Api/FightSearch.Api.csproj", "FightSearch.Api/"]
#COPY ["FightSearch.Service/FightSearch.Service.csproj", "FightSearch.Service/"]
#COPY ["FightSearch.Common/FightSearch.Common.csproj", "FightSearch.Common/"]
#COPY ["FightSearch.Repository.Sql/FightSearch.Repository.Sql.csproj", "FightSearch.Repository.Sql/"]
##RUN dotnet restore "FightSearch.Api/FightSearch.Api.csproj"
##COPY Api/Api.csproj Api/
#RUN dotnet restore
#COPY . .
##WORKDIR /src/Api
#WORKDIR /FightSearch.Api
#RUN dotnet build -c Release -o /app
#
#FROM builder AS publish
#RUN dotnet publish -c Release -o /app
#
#FROM base AS production
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "FightSearch.Api.dll"]
#
#asd
#FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
#WORKDIR /app
#
## copy csproj and restore as distinct layers
#COPY *.sln .
#COPY aspnetapp/*.csproj ./aspnetapp/
#RUN dotnet restore
#
## copy everything else and build app
#COPY aspnetapp/. ./aspnetapp/
#WORKDIR /app/aspnetapp
#RUN dotnet publish -c Release -o out
#
#
#FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
#WORKDIR /app
#COPY --from=build /app/aspnetapp/out ./
#ENTRYPOINT ["dotnet", "aspnetapp.dll"]
#


# For more info see: http://aka.ms/VSContainerToolingDockerfiles
#FROM microsoft/aspnetcore:2.2 AS base
#WORKDIR /app
#EXPOSE 80
#
#FROM microsoft/aspnetcore-build:2.2 AS builder
#WORKDIR /src
#COPY *.sln ./
#COPY Api/Api.csproj Api/
#RUN dotnet restore
#COPY . .
#WORKDIR /src/Api
#RUN dotnet build -c Release -o /app
#
#FROM builder AS publish
#RUN dotnet publish -c Release -o /app
#
#FROM base AS production
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "Api.dll"]
#