FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy the project files 
COPY FinanceCore/FinanceCore.Domain.csproj FinanceCore/
COPY FinanceCore.Application/FinanceCore.Application.csproj FinanceCore.Application/
COPY FinanceCore.Infrastructure/FinanceCore.Infrastructure.csproj FinanceCore.Infrastructure/
COPY FinanceCore.API/FinanceCore.API.csproj FinanceCore.API/

# install nuget dependencies
RUN dotnet restore FinanceCore.API/FinanceCore.API.csproj

COPY FinanceCore/ FinanceCore/
COPY FinanceCore.Application/ FinanceCore.Application/
COPY FinanceCore.Infrastructure/ FinanceCore.Infrastructure/
COPY FinanceCore.API/ FinanceCore.API/

# Publish the API
RUN dotnet publish FinanceCore.API/FinanceCore.API.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

USER $APP_UID
ENTRYPOINT ["dotnet", "FinanceCore.API.dll"]
