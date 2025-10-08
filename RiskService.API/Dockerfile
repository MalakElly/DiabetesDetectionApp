
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


COPY ["RiskService.API/RiskService.API.csproj", "RiskService.API/"]

RUN dotnet restore "RiskService.API/RiskService.API.csproj"


COPY . .
WORKDIR "/src/RiskService.API"
RUN dotnet publish "RiskService.API.csproj" -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .


ENV ASPNETCORE_URLS=http://+:80


EXPOSE 80

ENTRYPOINT ["dotnet", "RiskService.API.dll"]
