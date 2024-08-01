FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine as base
WORKDIR /app
EXPOSE 80
# Can't use this port in ASPNETCORE_URLS with TLS setup
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine as build
WORKDIR /src
COPY ["./Service.csproj", "."]
RUN dotnet restore "./Service.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Service.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base as final
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV ASPNETCORE_URLS="http://*:80"
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Service.dll"]
