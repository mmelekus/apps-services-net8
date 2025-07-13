# Apps and Services with .NET 8
Repository to store exercie files and labs for the book [Apps and Services with .NET 8 - Second Edition](https://learning.oreilly.com/library/view/apps-and-services/9781837637133/)

## Links
- [Apps and Services with .NET 8 (O'Reilly Books)](https://learning.oreilly.com/library/view/apps-and-services/9781837637133/)
  - [Book Code Samples (GitHub)](https://github.com/markjprice/apps-services-net8)
  - [Color images of the screenshots and diagrams used in the book](https://packt.link/gbp/9781837637133)
- [Exercise Files and Projects (GitHub)](https://github.com/mmelekus/apps-services-net8)
- [Azure Cosmos DB for NoSQL Documentation](https://learn.microsoft.com/en-us/azure/cosmos-db/nosql/)
  - [Develop locaccy using the Azure Cosmos DB emulator](https://learn.microsoft.com/en-us/azure/cosmos-db/how-to-develop-emulator?tabs=docker-linux%2Ccsharp&pivots=api-nosql)

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
    - User ID: SA (MY_SQL_USR)
    - Password: s3cret-Ninja (MY_SQL_PWD)
  - Azure Cosmos DB Emulator in Docker
    - Endpoint: https://localhost:8081/
    - Primary Key: C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==
    - Database: Northwind
    - Container: Products
    - Partition Key: /productId
  - GraphQL Tools
    - ChilliCream
      - **Hot Chocolate** Enables you to create GraphQL services for .NET ([GitHub repository for **Hot Chocolate**](https://github.com/ChilliCream/hotchocolate))
      - **Strawberry Shake** Enables you to create GraphQL clients for .NET
      - **Banana Cake Pop** Enables you to run queries and explore a GraphQL endpoint using a Monaco-based GraphQL IDE
      - **Green Donut** Enables better performance when loading data
  - SonarQube Cloud Projects: [SonarQube Cloud - Projects](https://sonarcloud.io/projects)
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

> ### Configuration with Environment Variables
> Setting up configuration with environment variables using a launchSettings.json file.
> *launchSettings.json file format*
> ```
> {
>   "profiles": {
>     "<Assembly Name>" : {
>       "commandName": "Project",
>       "environmentVariables": {
>         "<EnvVar1>": "<Value1>",
>         "<EnvVar2>": "<Value2>"
>        }
>      }
>    }  
>  }
> ```
>
> *e.g.:*
> ```
> {
>   "profiles": {
>     "Northwind.CosmosDb.SqlApi" : {
>       "commandName": "Project",
>       "environmentVariables": {
>         "MY_SQL_USR": "<Value1>",
>         "MY_SQL_PWD": "<Value2>"
>        }
>      }
>    }  
>  }
> ```
> *Reference the following NuGet packages in the project:*
> - Microsoft.Extensions.Configuration
> - Microsoft.Extensions.Configuration.EnvironmentVariables
>
> *Use the following code to reference the environment variables:*
> ```
>   var config = new ConfigurationBuilder()
>     .AddEnvironmentVarialbes()
>     .Build();
> ```
> *Retrieve the environment variables:*
> ```
> var userID = Environment.GetEnvironmentVariable("MY_SQL_USR");
> var password = Environment.GetEnvironmentVariable("MY_SQL_PWD");
> ```