# Library management

### Database name
Open **"appsettings.json"** in **"./LibraryManagement.WpfApp"** and change value of **"Database"** key.
That will be your Microsoft SQL database name.

### Creating migration
Now open **Package Manager Console (PMC)**, set ""Default project"" as **"LibraryManagement.DAL"**.

1. Creating Initial migration
  - Add-Migration Initial
  - Update-Database
2. Removing Last migration
  - Update-Database 0
  - Remove-Migration

Update Database sets all your migrations **"applied"** status to "false". Thus making it possible to remove them.
