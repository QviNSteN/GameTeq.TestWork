#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GameTeq.TestWork.LocalFilesService/GameTeq.TestWork.LocalFilesService.csproj", "GameTeq.TestWork.LocalFilesService/"]
RUN dotnet restore "GameTeq.TestWork.LocalFilesService/GameTeq.TestWork.LocalFilesService.csproj"
COPY . .
WORKDIR "/src/GameTeq.TestWork.LocalFilesService"
RUN dotnet build "GameTeq.TestWork.LocalFilesService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameTeq.TestWork.LocalFilesService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameTeq.TestWork.LocalFilesService.dll"]