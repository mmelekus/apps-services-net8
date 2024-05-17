
create procedure "Sales by Year" 
	@Beginning_Date DateTime, @Ending_Date DateTime AS
SELECT Orders.ShippedDate, Orders.OrderId, "Order Subtotals".Subtotal, DATENAME(yy,ShippedDate) AS Year
FROM Orders INNER JOIN "Order Subtotals" ON Orders.OrderId = "Order Subtotals".OrderId
WHERE Orders.ShippedDate Between @Beginning_Date And @Ending_Date

GO

