#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["VatViewer.API/VatViewer.API.csproj", "VatViewer.API/"]
COPY ["VatViewer.Shared/VatViewer.Shared.csproj", "VatViewer.Shared/"]
RUN dotnet restore "VatViewer.API/VatViewer.API.csproj"
COPY . .
WORKDIR "/src/VatViewer.API"
RUN dotnet build "VatViewer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VatViewer.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VatViewer.API.dll"]