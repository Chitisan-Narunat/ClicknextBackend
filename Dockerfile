FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN cp appsettings.Docker.json appsettings.json
RUN dotnet publish kanbanboardAPI.csproj -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .


EXPOSE 80

ENTRYPOINT ["dotnet", "kanbanboardAPI.dll"]
