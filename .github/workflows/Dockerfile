#FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine AS base

#FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build-env
FROM mcr.microsoft.com/dotnet/sdk:6.0

#FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

COPY ./build /app/build
COPY ./.github/workflows/start.sh /app

ENTRYPOINT ["/bin/sh", "/app/start.sh"]

