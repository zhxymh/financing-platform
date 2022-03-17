#!/bin/bash

cd /app/build/DbMigrator
dotnet Tank.Financing.DbMigrator.dll

sleep 2

cd /app/build/Web
dotnet Tank.Financing.Web.dll

