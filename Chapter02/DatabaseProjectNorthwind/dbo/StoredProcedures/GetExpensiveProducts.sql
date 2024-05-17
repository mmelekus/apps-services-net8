CREATE PROCEDURE [dbo].[GetExpensiveProducts]
  @price MONEY,
  @count INT OUT
AS
  PRINT 'Getting expensive products: ' + TRIM(CAST(@price AS NVARCHAR(10)))
  SELECT @count = COUNT(*)
  FROM [dbo].[Products]
    WHERE [UnitPrice] >= @price
  SELECT *
  FROM [dbo].[Products]
    WHERE [UnitPrice] >= @price
RETURN 0
