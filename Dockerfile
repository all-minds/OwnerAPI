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