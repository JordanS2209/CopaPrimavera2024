FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 10000
ENV ASPNETCORE_URLS=http://+:10000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CopaPrimavera.API/CopaPrimavera.API.csproj", "CopaPrimavera.API/"]
RUN dotnet restore "CopaPrimavera.API/CopaPrimavera.API.csproj"
COPY . .


WORKDIR "/src/CopaPrimavera.API"

RUN dotnet build "CopaPrimavera.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CopaPrimavera.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CopaPrimavera.API.dll"]
