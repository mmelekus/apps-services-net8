CREATE TABLE [dbo].[Orders] (
    [OrderId]        INT           IDENTITY (1, 1) NOT NULL,
    [CustomerId]     NCHAR (5)     NULL,
    [EmployeeId]     INT           NULL,
    [OrderDate]      DATETIME      NULL,
    [RequiredDate]   DATETIME      NULL,
    [ShippedDate]    DATETIME      NULL,
    [ShipVia]        INT           NULL,
    [Freight]        MONEY         CONSTRAINT [DF_Orders_Freight] DEFAULT ((0)) NULL,
    [ShipName]       NVARCHAR (40) NULL,
    [ShipAddress]    NVARCHAR (60) NULL,
    [ShipCity]       NVARCHAR (15) NULL,
    [ShipRegion]     NVARCHAR (15) NULL,
    [ShipPostalCode] NVARCHAR (10) NULL,
    [ShipCountry]    NVARCHAR (15) NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([OrderId] ASC),
    CONSTRAINT [FK_Orders_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([CustomerId]),
    CONSTRAINT [FK_Orders_Employees] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employees] ([EmployeeId]),
    CONSTRAINT [FK_Orders_Shippers] FOREIGN KEY ([ShipVia]) REFERENCES [dbo].[Shippers] ([ShipperId])
);


GO

CREATE NONCLUSTERED INDEX [EmployeeId]
    ON [dbo].[Orders]([EmployeeId] ASC);


GO

CREATE NONCLUSTERED INDEX [EmployeesOrders]
    ON [dbo].[Orders]([EmployeeId] ASC);


GO

CREATE NONCLUSTERED INDEX [CustomersOrders]
    ON [dbo].[Orders]([CustomerId] ASC);


GO

CREATE NONCLUSTERED INDEX [ShippersOrders]
    ON [dbo].[Orders]([ShipVia] ASC);


GO

CREATE NONCLUSTERED INDEX [CustomerId]
    ON [dbo].[Orders]([CustomerId] ASC);


GO

CREATE NONCLUSTERED INDEX [ShippedDate]
    ON [dbo].[Orders]([ShippedDate] ASC);


GO

CREATE NONCLUSTERED INDEX [ShipPostalCode]
    ON [dbo].[Orders]([ShipPostalCode] ASC);


GO

CREATE NONCLUSTERED INDEX [OrderDate]
    ON [dbo].[Orders]([OrderDate] ASC);


GO

