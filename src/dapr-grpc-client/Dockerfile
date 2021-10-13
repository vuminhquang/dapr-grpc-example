#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["dapr-grpc-client/dapr-grpc-client.csproj", "dapr-grpc-client/"]
RUN dotnet restore "dapr-grpc-client/dapr-grpc-client.csproj"
COPY . .
WORKDIR "/src/dapr-grpc-client"
RUN dotnet build "dapr-grpc-client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dapr-grpc-client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "dapr-grpc-client.dll"]