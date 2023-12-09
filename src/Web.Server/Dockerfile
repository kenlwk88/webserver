#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Web.Server/Web.Server.csproj", "Web.Server/"]
COPY ["Core.DataAccess/Core.DataAccess.csproj", "Core.DataAccess/"]
COPY ["Web.Application/Web.Application.csproj", "Web.Application/"]
COPY ["Core.Infrastructure/Core.Infrastructure.csproj", "Core.Infrastructure/"]
COPY ["Web.Domain/Web.Domain.csproj", "Web.Domain/"]
RUN dotnet restore "Web.Server/Web.Server.csproj"
COPY . .
WORKDIR "/src/Web.Server"
RUN dotnet build "Web.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.Server.dll"]