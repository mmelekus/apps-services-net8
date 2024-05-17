
create view "Summary of Sales by Quarter" AS
SELECT Orders.ShippedDate, Orders.OrderId, "Order Subtotals".Subtotal
FROM Orders INNER JOIN "Order Subtotals" ON Orders.OrderId = "Order Subtotals".OrderId
WHERE Orders.ShippedDate IS NOT NULL
--ORDER BY Orders.ShippedDate

GO

