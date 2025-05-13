using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Product.Core.Entities;
using Product.Core.Interfaces;
using Product.Infrastructure.Data.DTOs;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork Uow,IMapper mapper)
        {
            _uow = Uow;
            _mapper = mapper;
        }
        [HttpGet("get-all-category")]
        public async Task <ActionResult> Get()
        {
            var all_category = await _uow.CategoryRepository.GetAllAsync();
            if (all_category != null)
            {
                var result = _mapper.Map<IReadOnlyList<Category>, IReadOnlyList<ListCategoryDTO>>(all_category);
                return Ok(result);
            }
            return BadRequest("Not Found");
        }
        [HttpGet("get-category-by-id/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var category = await _uow.CategoryRepository.GetByIdAsync(id);
            if(category!=null)
                return Ok(_mapper.Map<Category,ListCategoryDTO>(category));
            return BadRequest($"Not found this id = [{id}");

        }
        [HttpPost("add-new-category")]
        public async Task<ActionResult> Post(CategoryDTO category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _mapper.Map<Category>(category);
                    
                    await _uow.CategoryRepository.AddAsync(result);
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update-category/{id}")]
        public async Task<ActionResult> Put(int id,CategoryDTO category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var exisiting_category = await _uow.CategoryRepository.GetByIdAsync(id);
                    if(exisiting_category!=null)
                    {
                        exisiting_category.Name = category.Name;
                        exisiting_category.Description = category.Description;
                    }
                    await _uow.CategoryRepository.UpdateAsync(exisiting_category);
                    return Ok(exisiting_category);
                }
                return BadRequest($"Category id [{id}] Not Found ");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("delete-category/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
            var exisiting_category = await _uow.CategoryRepository.GetByIdAsync(id);
                if (exisiting_category != null)
                {
                    await _uow.CategoryRepository.DeleteAsync(id);
                    return Ok($"this category [{exisiting_category.Name}] is deleted successfully");
                }
                    return BadRequest($"Category id [{id}] Not Found ");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
    
    }
