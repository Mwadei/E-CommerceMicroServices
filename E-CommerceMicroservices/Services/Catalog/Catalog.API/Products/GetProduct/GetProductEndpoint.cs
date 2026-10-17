namespace Catalog.API.Products.GetProduct
{
    public record GetProductRequest(int? pageNumber = 1, int? pageSize = 10);
    public record GetProductResponse(IEnumerable<Product> Products);

    public class GetProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters] GetProductRequest request ,ISender sender) =>
            {
                var query = request.Adapt<GetProductQuery>();

                var result = await sender.Send(query, CancellationToken.None);

                var response = result.Adapt<GetProductResult>();

                return Results.Ok(response);
            })
            .WithName("Get Product")
            .Produces<GetProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product")
            .WithDescription("Get Product");
        }
    }
}
