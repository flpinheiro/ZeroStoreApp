# Migration

to apply the migration, run the following command:

```sh
Add-Migration -name "InitialMigration" -Context ZeroStoreAppDbContext -Project ZeroStoreApp.Infra -OutputDir Data/Migrations -StartupProject ZeroStoreApp.CommandService -verbose
```

to update the database, run the following command:
```sh
Update-Database -Context ZeroStoreAppDbContext
```

# Database

The database used in this project is SQL Server. To change the database, you need to change the connection string in the `appsettings.json` file.