# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app
EXPOSE 8080
EXPOSE 8081
EXPOSE 5238


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["RazorPagesUI.csproj", "."]
RUN dotnet restore "./RazorPagesUI.csproj"

COPY . .

RUN dotnet build "./RazorPagesUI.csproj" -c Debug -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
RUN dotnet publish "./RazorPagesUI.csproj" -c Release -o /app/publish 

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app/publish
COPY --from=publish /app/publish .

COPY Test1.db .
ENTRYPOINT ["dotnet", "RazorPagesUI.dll"]