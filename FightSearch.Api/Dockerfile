FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["FightSearch3/FightSearch.Api.csproj", "FightSearch.Api/"]
COPY ["FightSearch.Common/FightSearch.Common.csproj", "FightSearch.Common/"]
RUN dotnet restore "FightSearch3/FightSearch.Api.csproj"
COPY . .
WORKDIR "/src/FightSearch3"
RUN dotnet build "FightSearch.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FightSearch.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FightSearch.Api.dll"]