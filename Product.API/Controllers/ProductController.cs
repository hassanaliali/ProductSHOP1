using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Entities;
using Product.Core.Interfaces;
using Product.Infrastructure.Data.DTOs;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork Uow,IMapper mapper)
        {
            _uow = Uow;
            _mapper = mapper;
            
        }

        [HttpGet("get-all-products")]
        public async Task<ActionResult> Get()
        {
            var all_products = await _uow.ProductRepository.GetAllAsync(x=>x.category);
            if (all_products != null)
            {
                var result = _mapper.Map<IReadOnlyList<ListProductDTO>>(all_products);
                return Ok(result);
            }
            return BadRequest("Not Found");
        }
        [HttpGet("get-products-by-categegory/{id}")]
        public async Task<ActionResult> GetByCategory(int id)
        {
            var all_products = await _uow.ProductRepository.GetAllAsync(x => x.CategoryId==id);
            if (all_products != null)
            {
                var result = _mapper.Map<IReadOnlyList<ListProductDTO>>(all_products);
                return Ok(result);
            }
            return BadRequest("Not Found");
        }
        [HttpGet("get-product-by-id/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var product =await _uow.ProductRepository.GetByIdAsync(id,x=>x.category);
            if(product != null)
                return Ok(_mapper.Map<ProductEntity,ProductDTO>(product));
            return BadRequest($"Not found this id = [{id}]");
        }
        [HttpPost("add-new-product")]
        public async Task<ActionResult> Post(AddProductDTO product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _mapper.Map<ProductEntity>(product);

                    await _uow.ProductRepository.AddAsync(result);
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update-product/{id}")]
        public async Task<ActionResult>Put(int id,UpdateProductDTO product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var exist_product = await _uow.ProductRepository.GetByIdAsync(id);
                    if(exist_product!=null)
                    {
                        exist_product.Name = product.Name;
                        exist_product.Description = product.Description;
                        exist_product.Price = product.Price;
                        exist_product.CategoryId = product.CategoryId;
                    }
                    await _uow.ProductRepository.UpdateAsync(exist_product);
                    return Ok(exist_product);
                }
                return BadRequest($"product id [{id}] Not Found ");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("delete-product")]
        public async Task<ActionResult>Delete(int id)
        {
            try
            {
                
                    var exist_product = await _uow.ProductRepository.GetByIdAsync(id);
                    if (exist_product != null)
                    {
                        await _uow.ProductRepository.DeleteAsync(id);
                        return Ok($"product is deleted successfully id [{id}]");
                    }
                return BadRequest($"product id [{id}] Not Found ");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
