#!/bin/bash

dotnet /app/DbMigrator/Tank.Financing.DbMigrator.dll

sleep 2

dotnet /app/Web/Tank.Financing.Web.dll

