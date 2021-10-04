#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["services/order/order.api/order.api.csproj", "services/order/order.api/"]
COPY ["services/order/order.infrastructure/order.infrastructure.csproj", "services/order/order.infrastructure/"]
COPY ["services/order/order.domain/order.domain.csproj", "services/order/order.domain/"]
COPY ["services/order/order.application/order.application.csproj", "services/order/order.application/"]
RUN dotnet restore "services/order/order.api/order.api.csproj"
COPY . .
WORKDIR "/src/services/order/order.api"
RUN dotnet build "order.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "order.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "order.api.dll"]
