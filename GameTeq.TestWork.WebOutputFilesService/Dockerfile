#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GameTeq.TestWork.WebOutputFilesService/GameTeq.TestWork.WebOutputFilesService.csproj", "GameTeq.TestWork.WebOutputFilesService/"]
RUN dotnet restore "GameTeq.TestWork.WebOutputFilesService/GameTeq.TestWork.WebOutputFilesService.csproj"
COPY . .
WORKDIR "/src/GameTeq.TestWork.WebOutputFilesService"
RUN dotnet build "GameTeq.TestWork.WebOutputFilesService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameTeq.TestWork.WebOutputFilesService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameTeq.TestWork.WebOutputFilesService.dll"]