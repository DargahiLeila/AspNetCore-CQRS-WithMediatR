# 🧠 ASP.NET Core CQRS Demo (Using MediatR)

This project demonstrates a manual implementation of the **CQRS (Command Query Responsibility Segregation)** pattern in ASP.NET Core, built on a classic **N-Tier architecture**. It separates commands and queries across distinct layers  using MediatR or other external libraries.

---

## 🎯 Project Purpose

- Showcase CQRS using MediatR
- Apply clean separation of concerns using N-Tier architecture
- Demonstrate layered design with Domain, DAL, BLL, and UI

---

## ⚙️ Technologies Used

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server (LocalDB)
- Dependency Injection
- Manual CQRS (Command/Query interfaces)
- N-Tier Architecture (UI, BLL, DAL, Domain)

---

## 🚀 How to Run

1. Clone the repository:
   ```bash
  git clone   https://github.com/DargahiLeila/AspNetCore-CQRS-WithMediatR.git
2.Open the solution file (.sln) in Visual Studio 2022 or later.

3.Make sure your SQL Server instance is running.

4.Create two separate SQL Server databases manually:

MyApp_WriteDB → used for write operations (commands)

MyApp_ReadDB → used for read operations (queries)

5.In both databases, create the required User table using the following SQL script:

CREATE TABLE [dbo].[TBL_Users] (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [Name] NVARCHAR(50),
    [IsDeleted] BIT NOT NULL
);

6.Update the connection strings in appsettings.json to match your SQL Server configuration. Example:

"ReadConnectionString": "Data Source=YourServerName;Initial Catalog=MyApp_ReadDB;TrustServerCertificate=True;User Id=*;Password=*;",

"WriteConnectionString": "Data Source=YourServerName;Initial Catalog=MyApp_WriteDB;TrustServerCertificate=True;User Id=*;Password=*;"

⚠️ Note: Replace Data Source, User Id, and Password with your own SQL Server credentials if different.


7.Run the project:

Press Ctrl + F5 or click Start Without Debugging

The browser will open and load the home page


🔄 Background Job: Sync Write → Read
To support physical separation of read and write operations in the CQRS pattern, this project includes a background job that runs every 1 minute and synchronizes data between the two databases:

Source: MyApp_WriteDB.dbo.TBL_Users

Target: MyApp_ReadDB.dbo.TBL_Users

The job performs a full refresh of the read-side table using the following SQL logic:

SET NOCOUNT ON;

BEGIN TRY
    BEGIN TRAN;

    TRUNCATE TABLE MyApp_ReadDB.dbo.TBL_Users;

    INSERT INTO MyApp_ReadDB.dbo.TBL_Users (Id, Name, IsDeleted)
    SELECT Id, Name, IsDeleted
    FROM MyApp_WriteDB.dbo.TBL_Users;

    COMMIT TRAN;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;

    DECLARE @Err NVARCHAR(4000) = ERROR_MESSAGE();
    RAISERROR(N'Sync Users failed: %s', 16, 1, @Err);
END CATCH;

🛠️ You can schedule this job using SQL Server Agent or any external scheduler depending on your deployment environment.
