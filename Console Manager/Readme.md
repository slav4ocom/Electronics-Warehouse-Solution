# Electronics Shop Warehouse manager

This is very simple console program that manages articles in warehouse.

The articles are added / listed / modified / removed  <i>( CRUD operations )</i> via command interface.

Compilled with .NET CORE 3.1

Project uses <b>MS SQL</b> server database to actually store articles.
Connection to SQL server is over TCP. Connection string is provided in <b>ElectronicsWarehouseContext.cs</b> OnConfiguring method.
For testing I use my MS SQL server on my home network. You can use remote server over internet.
This project is explanation how to work with Entity Framework Core.
Code first approach to create database is used.
When you change database Models, database is automatically updated with corresponding structure.

NuGet packages required: 

    Microsoft.EntityFrameworkCore.Design to create models

    Microsoft.EntityFrameworkCore.SqlServer to connect with database server

    Microsoft.EntityFrameworkCore.Tools to work with Package Manager Console inside Visual Studio

Print <b>help</b> to show help using commands.