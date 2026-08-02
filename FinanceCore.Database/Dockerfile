FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY FinanceCore.Database/FinanceCore.Database.csproj FinanceCore.Database/

RUN dotnet restore FinanceCore.Database/FinanceCore.Database.csproj

COPY FinanceCore.Database/ FinanceCore.Database/

RUN dotnet publish FinanceCore.Database/FinanceCore.Database.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final 

WORKDIR /app

COPY --from=build /app/publish .

COPY database/migrations ./migrations

USER $APP_UID
ENTRYPOINT ["dotnet", "FinanceCore.Database.dll"]
