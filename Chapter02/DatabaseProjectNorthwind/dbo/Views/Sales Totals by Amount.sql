
create view "Sales Totals by Amount" AS
SELECT "Order Subtotals".Subtotal AS SaleAmount, Orders.OrderId, Customers.CompanyName, Orders.ShippedDate
FROM 	Customers INNER JOIN 
		(Orders INNER JOIN "Order Subtotals" ON Orders.OrderId = "Order Subtotals".OrderId) 
	ON Customers.CustomerId = Orders.CustomerId
WHERE ("Order Subtotals".Subtotal >2500) AND (Orders.ShippedDate BETWEEN '19970101' And '19971231')

GO

