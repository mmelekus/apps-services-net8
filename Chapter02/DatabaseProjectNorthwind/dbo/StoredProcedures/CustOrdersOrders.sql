
CREATE PROCEDURE CustOrdersOrders @CustomerId nchar(5)
AS
SELECT OrderId, 
	OrderDate,
	RequiredDate,
	ShippedDate
FROM Orders
WHERE CustomerId = @CustomerId
ORDER BY OrderId

GO

