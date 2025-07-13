using System;
using HotChocolate.Subscriptions;
using Northwind.Common.DataContext.SqlServer;
using Northwind.Common.EntityModels.SqlServer.Models;
using Northwind.GraphQL.Service.Models;

namespace Northwind.GraphQL.Service;

public class Mutation
{
    public async Task<AddProductPayload> AddProductAsync(AddProductInput input, [Service] NorthwindDb dbContext)
    {
        // This could be a good place to use a tool like AutoMapper,
        // but we will do the mapping between two objects manually.
        Product product = new()
        {
            ProductName = input.ProductName,
            SupplierId = input.SupplierId,
            CategoryId = input.CategoryId,
            QuantityPerUnit = input.QuantityPerUnit,
            UnitPrice = input.UnitPrice,
            UnitsInStock = input.UnitsInStock,
            UnitsOnOrder = input.UnitsOnOrder,
            ReorderLevel = input.ReorderLevel,
            Discontinued = input.Discontinued
        };

        dbContext.Products.Add(product);
        int affectedRows = await dbContext.SaveChangesAsync();
        // We could use affectedRows to return an error or some other action if it is 0.
        return new AddProductPayload(product);
    }

    // public async Task<UpdateProductPayload> UpdateProductPriceAsync(UpdateProductPriceInput input, [Service] NorthwindDb dbContext)
    // {
    //     Product? product = await dbContext.Products.FindAsync(input.ProductId);
    //     int affectedRows = 0;
    //     if (product is not null)
    //     {
    //         product.UnitPrice = input.UnitPrice;
    //         affectedRows = await dbContext.SaveChangesAsync();
    //     }

    //     return new UpdateProductPayload(product, updated: affectedRows == 1);
    // }

    public async Task<UpdateProductPayload> UpdateProductUnitsAsync(UpdateProductUnitsInput input, [Service] NorthwindDb dbContext)
    {
        Product? product = await dbContext.Products.FindAsync(input.ProductId);
        int affectedRows = 0;
        if (product is not null)
        {
            product.UnitsInStock = input.UnitsInStock;
            product.UnitsOnOrder = input.UnitsOnOrder;
            product.ReorderLevel = input.ReorderLevel;
            affectedRows = await dbContext.SaveChangesAsync();
        }

        return new UpdateProductPayload(product, updated: affectedRows == 1);
    }

    public async Task<DeleteProductPayload> DeleteProductAsync(DeleteProductInput input, [Service] NorthwindDb dbContext)
    {
        Product? product = await dbContext.Products.FindAsync(input.ProductId);
        int affectedRows = 0;
        if (product is not null)
        {
            dbContext.Products.Remove(product);
            affectedRows = await dbContext.SaveChangesAsync();
        }

        return new DeleteProductPayload(deleted: affectedRows == 1);
    }

    public async Task<UpdateProductPayload> UpdateProductPriceAsync(UpdateProductPriceInput input, [Service] NorthwindDb dbContext, ITopicEventSender eventSender)
    {
        Product? product = await dbContext.Products.FindAsync(input.ProductId);
        int affectedRows = 0;
        if (product is not null)
        {
            if (input.UnitPrice < product.UnitPrice)
            {
                // If the product has been discounted, send a message to subscribers.
                ProductDiscount productDiscount = new()
                {
                    ProductId = product.ProductId,
                    OriginalUnitPrice = product.UnitPrice,
                    NewUnitPrice = input.UnitPrice
                };
                await eventSender.SendAsync(topicName: nameof(Subscription.OnProductDiscounted), message: productDiscount);
            }
            product.UnitPrice = input.UnitPrice;
            affectedRows = await dbContext.SaveChangesAsync();
        }

        return new UpdateProductPayload(product, updated: affectedRows == 1);
    }
}
