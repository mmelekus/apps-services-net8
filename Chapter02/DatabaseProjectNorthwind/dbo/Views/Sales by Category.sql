
create view "Sales by Category" AS
SELECT Categories.CategoryId, Categories.CategoryName, Products.ProductName, 
	Sum("Order Details Extended".ExtendedPrice) AS ProductSales
FROM 	Categories INNER JOIN 
		(Products INNER JOIN 
			(Orders INNER JOIN "Order Details Extended" ON Orders.OrderId = "Order Details Extended".OrderId) 
		ON Products.ProductId = "Order Details Extended".ProductId) 
	ON Categories.CategoryId = Products.CategoryId
WHERE Orders.OrderDate BETWEEN '19970101' And '19971231'
GROUP BY Categories.CategoryId, Categories.CategoryName, Products.ProductName
--ORDER BY Products.ProductName

GO

