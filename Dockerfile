FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy tất cả csproj
COPY DAL/DAL.csproj ./DAL/
COPY BLL/BLL.csproj ./BLL/
COPY Common/Common.csproj ./Common/
COPY GarenaFrondEnd/GarenaFrondEnd.csproj ./GarenaFrondEnd/
COPY Garena2/Garena2.csproj ./Garena2/
COPY *.sln .

# Restore packages
RUN dotnet restore

# Copy toàn bộ source code
COPY . .

# Build
WORKDIR /app/Garena2
RUN dotnet publish -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "Garena2.dll"]
