namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductResponse(bool isSucess);
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductCommand command, ISender sender) =>
            {
                var result = await sender.Send(command, CancellationToken.None);
                var response = result.Adapt<UpdateProductResult>();
                return Results.Ok(response);
            })
            .WithName("Update Product")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Product")
            .WithDescription("Update Product");
        }
    }
}
