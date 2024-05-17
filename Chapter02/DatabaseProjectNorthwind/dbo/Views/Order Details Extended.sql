
create view "Order Details Extended" AS
SELECT "Order Details".OrderId, "Order Details".ProductId, Products.ProductName, 
	"Order Details".UnitPrice, "Order Details".Quantity, "Order Details".Discount, 
	(CONVERT(money,("Order Details".UnitPrice*Quantity*(1-Discount)/100))*100) AS ExtendedPrice
FROM Products INNER JOIN "Order Details" ON Products.ProductId = "Order Details".ProductId
--ORDER BY "Order Details".OrderId

GO

