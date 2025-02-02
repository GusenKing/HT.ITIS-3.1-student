using Dotnet.Homeworks.Data.DatabaseContext;
using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Homeworks.DataAccess.Repositories;

public class ProductRepository : IProductRepository
{
    private AppDbContext DbContext { get; }

    public ProductRepository(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Products.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task DeleteProductByGuidAsync(Guid id, CancellationToken cancellationToken)
    {
        var product =
            await DbContext.Products
                .Where(x => x.Id.Equals(id))
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new KeyNotFoundException($"Product(id={id}) not found");

        DbContext.Products.Remove(product);
    }

    public async Task UpdateProductAsync(Product product, CancellationToken cancellationToken)
    {
        await Task.FromResult(DbContext.Products.Update(product));
    }

    public async Task<Guid> InsertProductAsync(Product product, CancellationToken cancellationToken)
    {
        return await Task.FromResult(DbContext.Products.Add(product).Entity.Id);
    }
}