# İş vaxtı üçün istifadə olunacaq kiçik ölçülü .NET 8 ASP.NET imici
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# İş qovluğunu /app olaraq təyin edirik
WORKDIR /app
# Konteynerin 8080 portunu kənara açırıq
EXPOSE 8080

# Layihəni build etmək (yığmaq) üçün SDK imici
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# Build prosesi üçün iş qovluğu
WORKDIR /src

# .csproj fayllarını kopyalayırıq ki, dependency-lər yüklənsin (restore)
COPY ["Master.API/Master.API.csproj", "Master.API/"]
COPY ["Master.Application/Master.Application.csproj", "Master.Application/"]
COPY ["Master.Domain/Master.Domain.csproj", "Master.Domain/"]
COPY ["Master.Infrastructure/Master.Infrastructure.csproj", "Master.Infrastructure/"]
# Paketləri (Nuget) bərpa edirik
RUN dotnet restore "Master.API/Master.API.csproj"

# Bütün layihə kodunu kopyalayırıq
COPY . .
# API layihə qovluğuna keçirik
WORKDIR "/src/Master.API"
# Layihəni Release modunda build edib /app/build qovluğuna yığırıq
RUN dotnet build "Master.API.csproj" -c Release -o /app/build

# Publish (yayımlamaq) mərhələsi
FROM build AS publish
# Faylları yayımlayırıq ki, runtime-da işlətmək olsun
RUN dotnet publish "Master.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Son mərhələ - işlək vəziyyət
FROM base AS final
# İş qovluğuna keçirik
WORKDIR /app
# Publish olunmuş faylları build stage-dən bura kopyalayırıq
COPY --from=publish /app/publish .
# Konteyner işə düşəndə hansı komandanın işləyəcəyini təyin edirik
ENTRYPOINT ["dotnet", "Master.API.dll"]
