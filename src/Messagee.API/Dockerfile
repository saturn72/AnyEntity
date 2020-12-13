#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Messagee.API/Messagee.API.csproj", "Messagee.API/"]
RUN dotnet restore "Messagee.API/Messagee.API.csproj"
COPY . .
WORKDIR "/src/Messagee.API"
RUN dotnet build "Messagee.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Messagee.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Messagee.API.dll"]
