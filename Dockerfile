FROM mcr.microsoft.com/dotnet/sdk:8.0 AS builder
WORKDIR /app
COPY ./PartyWebAppClient/PartyWebAppClient.csproj ./PartyWebAppClient/
COPY ./PartyWebAppCommon/PartyWebAppCommon.csproj ./PartyWebAppCommon/
COPY ./PartyWebAppServer/PartyWebAppServer.csproj ./PartyWebAppServer/
COPY ./PartyWebApp.sln .
WORKDIR /app/PartyWebAppServer
RUN dotnet restore
WORKDIR /app
COPY . .
RUN dotnet publish PartyWebAppServer/PartyWebAppServer.csproj -c release -o /published

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=builder /published .
ENTRYPOINT ["./PartyWebAppServer"]
