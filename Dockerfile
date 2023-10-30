FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR "/src/Api"

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR "/src/Api"
COPY --from=build /src/Api/out .
ENTRYPOINT ["dotnet", "SecurityApi.dll"]

#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR "/app/src/Api"
#COPY . .
#RUN dotnet publish "SecurityApi.csproj" -c Release -o /app

#FROM mcr.microsoft.com/dotnet/aspnet:6.0
#WORKDIR /app
#COPY --from=build /app ./
#ENTRYPOINT ["dotnet", "SecurityApi.dll"]


#FROM ghcr.io/architecture-it/net:6.0-sdk as build
#WORKDIR /app
#COPY . .
#RUN dotnet restore
#WORKDIR "/app/src/Api"
#RUN dotnet build "SecurityApi.csproj" -c Release -o /app/build

#FROM build AS publish
#RUN dotnet publish "SecurityApi.csproj" -c Release -o /app/publish

#FROM ghcr.io/architecture-it/net:6.0
#COPY --from=publish /app/publish .

#ENTRYPOINT ["dotnet", "SecurityApi.dll"]
