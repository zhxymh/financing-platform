FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY ./tj-financing /app
COPY ./.github/workflows/start.sh /app

RUN dotnet publish tj-financing/src/Tank.Financing.DbMigrator/Tank.Financing.DbMigrator.csproj --configuration Release -o build/DbMigrator \
    && dotnet publish tj-financing/src/Tank.Financing.Web/Tank.Financing.Web.csproj --configuration Release -o build/Web 

ENTRYPOINT ["/bin/sh", "/app/start.sh"]
