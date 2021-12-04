#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HARIA.StateKeeper/HARIA.StateKeeper.csproj", "HARIA.StateKeeper/"]
RUN dotnet restore "HARIA.StateKeeper/HARIA.StateKeeper.csproj"
COPY . .
WORKDIR "/src/HARIA.StateKeeper"
RUN dotnet build "HARIA.StateKeeper.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HARIA.StateKeeper.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HARIA.StateKeeper.dll"]