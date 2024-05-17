
create view "Quarterly Orders" AS
SELECT DISTINCT Customers.CustomerId, Customers.CompanyName, Customers.City, Customers.Country
FROM Customers RIGHT JOIN Orders ON Customers.CustomerId = Orders.CustomerId
WHERE Orders.OrderDate BETWEEN '19970101' And '19971231'

GO

