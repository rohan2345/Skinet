using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController(IGenericRepository<Product> repo) : BaseAPIController
{
   

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecParams specParams)
    {
       // return Ok(await repo.GetProductsAsync(brand,type,sort));using non-Genric Repository
          var spec= new ProductSpecification(specParams);
          //var products= await repo.ListAsync(spec); shiftet BASEAPI COntroller
         // var count=await repo.CountAsync(spec); shiftet BASEAPI COntroller
          //var pagination=new Pagination<Product>(specParams.PageIndex,specParams.PageSize,count,products);shiftet BASEAPI COntroller

          return await CreatePagedResult(repo,spec,specParams.PageIndex,specParams.PageSize);
          // return Ok(await repo.ListAllAsync()); //using Genric Repository
    }

    [HttpGet("{id:int}")] //api/products/2
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product= await repo.GetByIdAsync(id);
        if(product==null) return NotFound();
        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repo.Add(product);
        if (await repo.SaveAllAsync())
        {
            return CreatedAtAction("GetProduct",new {id=product.Id},product);
        }
        
        return BadRequest("Problem creating product");

    }
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id,Product product)
    {
        if(product.Id!=id || !ProductExist(id))
            return BadRequest("Cannot update this product");
        repo.Update(product);
        if(await repo.SaveAllAsync())
        {
            return NoContent();
        }
        return BadRequest("Problem updating product");

    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product= await repo.GetByIdAsync(id);
        if(product==null) return NotFound();
        repo.Remove(product);
        if(await repo.SaveAllAsync())
        {
            return NoContent();
        }
        return BadRequest("Problem deleting product");
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        //return Ok(await repo.GetBrandAsync()); TODO: Implement 
        var spec=new BrandListSpecification();
         return Ok(await repo.ListAsync(spec));
    }
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        //return Ok(await repo.GetTypesAsync());
        var spec=new TypeListSpecification();
        return Ok(await repo.ListAsync(spec));
        
    }
    private bool ProductExist(int id)
    {
        return repo.Exists(id);

    }

}
