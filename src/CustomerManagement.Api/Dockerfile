#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/CustomerManagement.Api/CustomerManagement.Api.csproj", "src/CustomerManagement.Api/"]
COPY ["src/CustomerManagement.Infrastructure/CustomerManagement.Infrastructure.csproj", "src/CustomerManagement.Infrastructure/"]
COPY ["src/CustomerManagement.Domain/CustomerManagement.Domain.csproj", "src/CustomerManagement.Domain/"]
RUN dotnet restore "src/CustomerManagement.Api/CustomerManagement.Api.csproj"
COPY . .
WORKDIR "/src/src/CustomerManagement.Api"
RUN dotnet build "CustomerManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerManagement.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerManagement.Api.dll"]
