
create procedure "Employee Sales by Country" 
@Beginning_Date DateTime, @Ending_Date DateTime AS
SELECT Employees.Country, Employees.LastName, Employees.FirstName, Orders.ShippedDate, Orders.OrderId, "Order Subtotals".Subtotal AS SaleAmount
FROM Employees INNER JOIN 
	(Orders INNER JOIN "Order Subtotals" ON Orders.OrderId = "Order Subtotals".OrderId) 
	ON Employees.EmployeeId = Orders.EmployeeId
WHERE Orders.ShippedDate Between @Beginning_Date And @Ending_Date

GO

