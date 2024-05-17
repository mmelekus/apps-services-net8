CREATE TABLE [dbo].[Categories] (
    [CategoryId]   INT           IDENTITY (1, 1) NOT NULL,
    [CategoryName] NVARCHAR (15) NOT NULL,
    [Description]  NTEXT         NULL,
    [Picture]      IMAGE         NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([CategoryId] ASC)
);


GO

CREATE NONCLUSTERED INDEX [CategoryName]
    ON [dbo].[Categories]([CategoryName] ASC);


GO

