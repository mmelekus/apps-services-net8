# Apps and Services with .NET 8
Repository to store exercie files and labs for the book [Apps and Services with .NET 8 - Second Edition](https://learning.oreilly.com/library/view/apps-and-services/9781837637133/)

## Links
- [Apps and Services with .NET 8 (O'Reilly Books)](https://learning.oreilly.com/library/view/apps-and-services/9781837637133/)
  - [Book Code Samples (GitHub)](https://github.com/markjprice/apps-services-net8)
  - [Color images of the screenshots and diagrams used in the book](https://packt.link/gbp/9781837637133)
- [Exercise Files and Projects (GitHub)](https://github.com/mmelekus/apps-services-net8)

## Technologies
- Framework: .NET 8
- Development Language: C# 12
- Data:
  - SQL Server (local)
  - Azure SQL Server in Azure
    - Connection String: `"Server=tcp:apps-services-book-mm.database.windows.net,1433;Initial Catalog=Northwind;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication="Active Directory Default";`
    - User ID: *Private*
    - Password: *Private*
  - Azure SQL Edge in Docker
    - Connection String: `"Data Source=tcp:127.0.0.1,1433;Initial Catalog=Northwind;User ID=SA;Password=s3cret-Ninja;Multiple Active Result Sets=True;Connect Timeout=3;Trust Server Certificate=True"`
    - User ID: SA
    - Password: s3cret-Ninja
- IDE's:
  - Visual Studio 2022
  - Visual Studio Code 1.88
- Version Control: Git 2.43
- Container Engine: Docker Desktop 4.29

## Notes
> ### Port Numbering
> - `http://localhost:5[chapternumber]2/`
> - `https://localhost:5[chapternumber]1/`
> - Example: Website for *Chapter 14* becomes: `https://localhost:5141/`

> ### Windows App SDK
> - For creating Windows 10 and 11 applications using a unified API and toolset.
> - [Windows App SDK](https://learn.microsoft.com/en-us/windows/apps/windows-app-sdk/)