
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]//這是一個屬性，標記該控制器將用於處理 API 請求。
    [Route("api/[controller]")]//這是路由屬性，它定義了到達此控制器的 URL 模式。所有控制器為API開頭
    public class ProductsController : ControllerBase //該類別繼承自 ControllerBase 類別。ControllerBase 類別提供了一些基本的功能，如處理 HTTP 請求和生成 HTTP 響應。
    {
       
        private readonly IProductRepository repo;

        public ProductsController(IProductRepository repo)
        {
            this.repo = repo;
           

        }

        [HttpGet]

        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await repo.GetProductsAsync();
            return Ok(products);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>>GetProduct(int id)
        {

            return await repo.GetProductByIdAsync(id);

        }


        
        [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>>GetProductBrands()
        {

            return Ok(await repo.GetProductBrandsAsync());

        }


                
        [HttpGet("types")]
        public async Task<ActionResult<ProductType>>GetProductTypes()
        {

            return Ok(await repo.GetProductTypesAsync());

        }
    }
}