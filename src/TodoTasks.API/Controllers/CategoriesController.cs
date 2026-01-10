using Microsoft.AspNetCore.Mvc;
using TodoTasks.Domain.Entities;
using TodoTasks.Application.Interfaces;

[ApiController]
[Route("api/[Controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("Categories")]
    public async Task<ActionResult<Category>> GetAllAsync()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategoryAsync(int id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        return NoContent();
    }

}