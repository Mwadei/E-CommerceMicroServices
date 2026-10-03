namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductcommand(Guid id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsDeleted);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductcommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Product id is required.");
        }
    }

    internal class DeleteProductCommandHandler(IDocumentSession session)
        : ICommandHandler<DeleteProductcommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductcommand command, CancellationToken cancellationToken)
        {
            session.Delete<Product>(command.id);
            await session.SaveChangesAsync();

            return new DeleteProductResult(true);
        }
    }
}
