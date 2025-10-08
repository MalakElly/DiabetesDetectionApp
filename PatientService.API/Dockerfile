FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY PatientService.Core/PatientService.Core.csproj PatientService.Core/
COPY PatientService.Infrastructure/PatientService.Infrastructure.csproj PatientService.Infrastructure/
COPY PatientService.API/PatientService.API.csproj PatientService.API/

RUN dotnet restore "PatientService.API/PatientService.API.csproj"

COPY PatientService.Core/ PatientService.Core/
COPY PatientService.Infrastructure/ PatientService.Infrastructure/
COPY PatientService.API/ PatientService.API/

WORKDIR /src/PatientService.API
RUN dotnet publish "PatientService.API.csproj" -c Release -o /app/publish



FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app


ENV ASPNETCORE_URLS=http://+:80

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PatientService.API.dll"]
