CREATE PROCEDURE CustOrderHist @CustomerId nchar(5)
AS
SELECT ProductName, Total=SUM(Quantity)
FROM Products P, [Order Details] OD, Orders O, Customers C
WHERE C.CustomerId = @CustomerId
AND C.CustomerId = O.CustomerId AND O.OrderId = OD.OrderId AND OD.ProductId = P.ProductId
GROUP BY ProductName

GO

