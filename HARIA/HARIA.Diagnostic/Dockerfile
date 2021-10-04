#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["HARIA.Diagnostic/HARIA.Diagnostic.csproj", "HARIA.Diagnostic/"]
RUN dotnet restore "HARIA.Diagnostic/HARIA.Diagnostic.csproj"
COPY . .
WORKDIR "/src/HARIA.Diagnostic"
RUN dotnet build "HARIA.Diagnostic.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HARIA.Diagnostic.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HARIA.Diagnostic.dll"]