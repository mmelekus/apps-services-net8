
create view "Alphabetical list of products" AS
SELECT Products.*, Categories.CategoryName
FROM Categories INNER JOIN Products ON Categories.CategoryId = Products.CategoryId
WHERE (((Products.Discontinued)=0))

GO

