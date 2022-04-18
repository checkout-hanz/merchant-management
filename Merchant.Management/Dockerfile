#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Merchant.Management/Merchant.Management.csproj", "Merchant.Management/"]
RUN dotnet restore "Merchant.Management/Merchant.Management.csproj"
COPY . .
WORKDIR "/src/Merchant.Management"
RUN dotnet build "Merchant.Management.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Merchant.Management.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Merchant.Management.dll"]