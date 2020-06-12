## Install EF tools globally
```
dotnet tool install --global dotnet-ef
```

## Run EF initial migration (create DB table)
```
dotnet ef migrations add InitialMigration
dotnet ef database update
```