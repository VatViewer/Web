#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["VatViewer.Jobs.Datafeed/VatViewer.Jobs.Datafeed.csproj", "VatViewer.Jobs.Datafeed/"]
COPY ["VatViewer.Shared/VatViewer.Shared.csproj", "VatViewer.Shared/"]
RUN dotnet restore "VatViewer.Jobs.Datafeed/VatViewer.Jobs.Datafeed.csproj"
COPY . .
WORKDIR "/src/VatViewer.Jobs.Datafeed"
RUN dotnet build "VatViewer.Jobs.Datafeed.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VatViewer.Jobs.Datafeed.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VatViewer.Jobs.Datafeed.dll"]