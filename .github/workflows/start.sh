#!/bin/bash

cd /app/DbMigrator
dotnet Tank.Financing.DbMigrator.dll

sleep 2

cd /app/Web
dotnet Tank.Financing.Web.dll

