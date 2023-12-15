
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext context;

        //構造函數
        public ProductRepository(StoreContext context)
        {
            this.context = context;

        }


        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await context.Products
            .Include(p => p.ProductType)//navigation properties
            .Include(p => p.ProductBrand)
            .FirstOrDefaultAsync(p =>p.id ==id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await context.Products
            .Include(p => p.ProductType)//navigation properties
            .Include(p => p.ProductBrand)
            .ToListAsync();



        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await context.ProductBrands.ToListAsync();
        }
        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await context.ProductTypes.ToListAsync();
        }
    }
}