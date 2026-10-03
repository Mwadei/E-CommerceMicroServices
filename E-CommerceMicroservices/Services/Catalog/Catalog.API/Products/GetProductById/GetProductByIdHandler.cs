namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid ProductId) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);

    internal class GetProductByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await session.LoadAsync<Product>(query.ProductId, cancellationToken);

            if (result is null)
            {
                throw new ProductNotFoundException();
            }

            return new GetProductByIdResult(result);

        }
    }
}
