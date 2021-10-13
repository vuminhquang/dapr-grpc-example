#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["grpc-server/grpc-server.csproj", "grpc-server/"]
RUN dotnet restore "grpc-server/grpc-server.csproj"
COPY . .
WORKDIR "/src/grpc-server"
RUN dotnet build "grpc-server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "grpc-server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "grpc-server.dll"]