#FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine AS base

#FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build-env

FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

COPY ./build/Web /app/Web
COPY ./build/DbMigrator /app/DbMigrator

#RUN set -x; dotnet /app/DbMigrator/Tank.Financing.DbMigrator.dll \
#    && dotnet /app/Web/Tank.Financing.Web.dll

#ENTRYPOINT ["dotnet", "/app/DbMigrator/Tank.Financing.DbMigrator.dll"]

#ENTRYPOINT ["dotnet", "/app/Web/Tank.Financing.Web.dll"]

