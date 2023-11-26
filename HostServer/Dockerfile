FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Host/Server/Host.Server.csproj", "Host/Server/"]
COPY ["Host/Client/Host.Client.csproj", "Host/Client/"]
COPY ["Host/Shared/Host.Shared.csproj", "Host/Shared/"]
RUN dotnet restore "Host/Server/Host.Server.csproj"
COPY . .
WORKDIR "/src/Host/Server"
RUN dotnet build "Host.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Host.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Host.Server.dll"]
