#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5106

ENV DOTNET_URLS=http://+:5106

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ExampleRestAPI/ExampleRestAPI.csproj", "ExampleRestAPI/"]
RUN dotnet restore "ExampleRestAPI/ExampleRestAPI.csproj"
COPY . .
WORKDIR "/src/ExampleRestAPI"
RUN dotnet build "ExampleRestAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExampleRestAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExampleRestAPI.dll"]