
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["NotesService.API/NotesService.API.csproj", "NotesService.API/"]

RUN dotnet restore "NotesService.API/NotesService.API.csproj"

COPY . .
WORKDIR "/src/NotesService.API"
RUN dotnet publish "NotesService.API.csproj" -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .


ENV ASPNETCORE_URLS=http://+:80


EXPOSE 80

ENTRYPOINT ["dotnet", "NotesService.API.dll"]
