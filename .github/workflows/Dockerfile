FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine AS base

WORKDIR /app

COPY ./build/Web /app
COPY ./build/DbMigrator /app

#RUN set -x; dotnet /app/DbMigrator/Tank.Financing.DbMigrator.dll \
#    && dotnet /app/Web/Tank.Financing.Web.dll

