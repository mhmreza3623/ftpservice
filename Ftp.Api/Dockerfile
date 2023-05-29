#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["KalaResan.Ftp/KalaResan.Ftp.Api.csproj", "KalaResan.Ftp/"]
COPY ["KalaResan.Ftp.Domain/KalaResan.Ftp.Domain.csproj", "KalaResan.Ftp.Domain/"]
COPY ["KalaResan.Ftp.Infrastructure/KalaResan.Ftp.Infrastructure.csproj", "KalaResan.Ftp.Infrastructure/"]
RUN dotnet restore "KalaResan.Ftp/KalaResan.Ftp.Api.csproj"
COPY . .
WORKDIR "/src/KalaResan.Ftp"
RUN dotnet build "KalaResan.Ftp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KalaResan.Ftp.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KalaResan.Ftp.Api.dll"]