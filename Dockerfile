# See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# WORKDIR /app
# EXPOSE 80
# EXPOSE 443

# FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
# WORKDIR /src
# COPY [".", "OwnerMicrosservice/"]
# RUN dotnet restore "OwnerMicrosservice/CustomerMicrosservice/OwnerMicrosservice.csproj"
# COPY . .
# WORKDIR "/src/OwnerMicrosservice"
# RUN dotnet build "CustomerMicrosservice/OwnerMicrosservice.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "CustomerMicrosservice/OwnerMicrosservice.csproj" -c Release -o /app/publish /p:UseAppHost=false

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "OwnerMicrosservice.dll"]

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
EXPOSE 7041
EXPOSE 5288
WORKDIR /src
COPY [".", "OwnerAPI/"]
RUN dotnet restore "OwnerAPI/CustomerMicrosservice/OwnerMicrosservice.csproj"
COPY . .
WORKDIR "/src/OwnerAPI"
RUN dotnet dev-certs https --clean
RUN dotnet dev-certs https --verbose
RUN dotnet build "./CustomerMicrosservice/OwnerMicrosservice.csproj" -c Release -o /app/build
RUN dotnet publish "./CustomerMicrosservice/OwnerMicrosservice.csproj" -c Release -o /app/publish /p:UseAppHost=false
WORKDIR /app/CustomerMicrosservice/
ENTRYPOINT ["dotnet", "run"]