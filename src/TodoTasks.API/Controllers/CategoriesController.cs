using Microsoft.AspNetCore.Mvc;
using TodoTasks.Domain.Entities;
using TodoTasks.Application.Interfaces;
using TodoTasks.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<Category>>> GetAllAsync(
        [FromQuery][Range(1, int.MaxValue)] int pageNumber = 1, 
        [FromQuery][Range(1, 100)] int pageSize = 20)
    {
        var request = new PaginationRequest { PageNumber = pageNumber, PageSize = pageSize };
        var categories = await _categoryService.GetPagedCategoriesAsync(request);
        return Ok(categories);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Category>> GetCategoryAsync(int id)
    {
        var category = await _categoryService.GetCategoryAsync(id);
        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategoryAsync([FromBody] CategorySaveRequest request)
    {
        var category = await _categoryService.CreateCategoryAsync(request);
        return CreatedAtAction(nameof(GetCategoryAsync), new { id = category.Id }, category);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCategoryAsync(int id, [FromBody] CategoryUpdateRequest request)
    {
        await _categoryService.UpdateCategoryAsync(id,request);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCategoryAsync(int id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        return NoContent();
    }
}