CREATE TABLE [dbo].[Products] (
    [ProductId]       INT           IDENTITY (1, 1) NOT NULL,
    [ProductName]     NVARCHAR (40) NOT NULL,
    [SupplierId]      INT           NULL,
    [CategoryId]      INT           NULL,
    [QuantityPerUnit] NVARCHAR (20) NULL,
    [UnitPrice]       MONEY         CONSTRAINT [DF_Products_UnitPrice] DEFAULT ((0)) NULL,
    [UnitsInStock]    SMALLINT      CONSTRAINT [DF_Products_UnitsInStock] DEFAULT ((0)) NULL,
    [UnitsOnOrder]    SMALLINT      CONSTRAINT [DF_Products_UnitsOnOrder] DEFAULT ((0)) NULL,
    [ReorderLevel]    SMALLINT      CONSTRAINT [DF_Products_ReorderLevel] DEFAULT ((0)) NULL,
    [Discontinued]    BIT           CONSTRAINT [DF_Products_Discontinued] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([ProductId] ASC),
    CONSTRAINT [CK_Products_UnitPrice] CHECK ([UnitPrice]>=(0)),
    CONSTRAINT [CK_ReorderLevel] CHECK ([ReorderLevel]>=(0)),
    CONSTRAINT [CK_UnitsInStock] CHECK ([UnitsInStock]>=(0)),
    CONSTRAINT [CK_UnitsOnOrder] CHECK ([UnitsOnOrder]>=(0)),
    CONSTRAINT [FK_Products_Categories] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([CategoryId]),
    CONSTRAINT [FK_Products_Suppliers] FOREIGN KEY ([SupplierId]) REFERENCES [dbo].[Suppliers] ([SupplierId])
);


GO

CREATE NONCLUSTERED INDEX [ProductName]
    ON [dbo].[Products]([ProductName] ASC);


GO

CREATE NONCLUSTERED INDEX [SuppliersProducts]
    ON [dbo].[Products]([SupplierId] ASC);


GO

CREATE NONCLUSTERED INDEX [CategoryId]
    ON [dbo].[Products]([CategoryId] ASC);


GO

CREATE NONCLUSTERED INDEX [SupplierId]
    ON [dbo].[Products]([SupplierId] ASC);


GO

CREATE NONCLUSTERED INDEX [CategoriesProducts]
    ON [dbo].[Products]([CategoryId] ASC);


GO

