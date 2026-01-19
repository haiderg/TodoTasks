using Xunit;
using FluentAssertions;
using Moq;
using TodoTasks.Domain.Entities;
using TodoTasks.Domain.Repositories;
using TodoTasks.Domain.ValueObjects;
using TodoTasks.Domain.Enums;


namespace TodoTasks.Application.Tests.Services;

public class CategoryServiceTests
{
    [Fact]
    public async Task CreateCategoryAsync_WithValidRequest_ShouldCreateAndReturnCategory()
    {
        //Arrange
        var mockRepository = new Mock<ICategoryRepository>();
        var categoryService = new CategoryService(mockRepository.Object);

        var request = new CategorySaveRequest
        {
            Name = "Test Category",
            Color = TaskColorEnum.Yellow,
            Description = "Test Description"
        };

        Category capturedCategory = null;
        mockRepository.Setup(r => r.AddAsync(It.IsAny<Category>()))
            .Callback<Category>(c => capturedCategory = c)
            .ReturnsAsync((Category c) => c);

        //Act
        var result = await categoryService.CreateCategoryAsync(request);

        //Assert
        mockRepository.Verify(c => c.AddAsync(It.Is<Category>(
            x => x.Name.Equals("Test Category") &&
            x.Description == "Test Description" &&
            x.Color == TaskColorEnum.Yellow)), Times.Once);
    }

    [Fact]
    public async Task GetAllCategoriesAsync_WhenCalled_ShouldReturnAllCategories()
    {
        //Arrange
        var mockRepository = new Mock<ICategoryRepository>();
        var categoryService = new CategoryService(mockRepository.Object);

        var categories = new List<Category>
        {
            Category.Create( new CategorySaveRequest{ Color = TaskColorEnum.Yellow, Name= "First", Description = "First Descripiton"}),
            Category.Create( new CategorySaveRequest{ Color = TaskColorEnum.Green, Name= "Second", Description = "Second Descripiton"}),
            Category.Create( new CategorySaveRequest{ Color = TaskColorEnum.Red, Name= "Third", Description = "Third Descripiton"})
        };

        mockRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(categories);

        //Act
        var result = await categoryService.GetAllCategoriesAsync();

        //Assert
        result.Count().Should().Be(3);
    }

    [Fact]
    public async Task GetCategoryAsync_WithValidId_ShouldReturnCategory()
    {
        // Arrange
        var mockRepository = new Mock<ICategoryRepository>();
        var service = new CategoryService(mockRepository.Object);
        var categoryId = 1;
        var expectedCategory = Category.Create(new CategorySaveRequest
        {
            Name = "Test",
            Color = TaskColorEnum.Red
        });

        mockRepository.Setup(r => r.GetByIdAsync(categoryId)).ReturnsAsync(expectedCategory);

        // Act
        var result = await service.GetCategoryAsync(categoryId);

        // Assert
        result.Should().Be(expectedCategory);
        mockRepository.Verify(r => r.GetByIdAsync(categoryId), Times.Once);
    }

    [Fact]
    public async Task GetCategoryAsync_WithInvalidId_ShouldReturnNull()
    {
        //Arrange
        Mock<ICategoryRepository>? mockRepository = new Mock<ICategoryRepository>();
        var service = new CategoryService(mockRepository.Object);
        int categoryId = -999;
        mockRepository.Setup(r => r.GetByIdAsync(categoryId)).ReturnsAsync((Category?)null);

        //Act
        var result = await service.GetCategoryAsync(categoryId);

        //Assert
        result.Should().BeNull();
        mockRepository.Verify(r => r.GetByIdAsync(categoryId), Times.Once);
    }

    [Fact]
    public async Task UpdateCategoryAsnync_WithValidId_ShouldUpdateCategory()
    {
        //Arrange
        var mockRepository = new Mock<ICategoryRepository>();
        var categoryService = new CategoryService(mockRepository.Object);
        var existingCategory = Category.Create(new CategorySaveRequest { Name = "Old Name", Color = TaskColorEnum.Yellow, Description = "Description" });

        int categoryId = 1;
        mockRepository.Setup(x => x.GetByIdAsync(categoryId)).ReturnsAsync(existingCategory);
        var categoryUpdateRequest = new CategoryUpdateRequest { Name = "Updated Name", Color = TaskColorEnum.Red, Description = "Updated Description" };

        //Act
        await categoryService.UpdateCategoryAsync(categoryId, categoryUpdateRequest);

        //Assert
        existingCategory.Name.Should().Be("Updated Name");
        mockRepository.Verify(r => r.GetByIdAsync(1), Times.Once);
        mockRepository.Verify(x => x.UpdateAsync(existingCategory), Times.Once);
    }

    [Fact]
    public async Task UpdateCategoryAsync_WithInvalidId_ShouldThrowArgumentException()
    {
        // Arrange
        var mockRepository = new Mock<ICategoryRepository>();
        var service = new CategoryService(mockRepository.Object);
        var updateRequest = new CategoryUpdateRequest { Name = "New Name" };

        int categoryId = -1;
        mockRepository.Setup(r => r.GetByIdAsync(categoryId)).ReturnsAsync((Category?)null);

        // Act
        var act = () => service.UpdateCategoryAsync(categoryId, updateRequest);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("Category not found");
        mockRepository.Verify(r => r.GetByIdAsync(categoryId), Times.Once);
        mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Category>()), Times.Never);
    }

    [Fact]
    public async Task DeleteCategoryAsync_WithValidId_ShouldDeleteCategory()
    {
        //Arrange
        var mockRepository = new Mock<ICategoryRepository>();
        var service = new CategoryService(mockRepository.Object);

        int categoryId = 1;
        mockRepository.Setup(x => x.DeleteAsync(categoryId));

        //Act
        await service.DeleteCategoryAsync(categoryId);

        //Assert
        mockRepository.Verify(x => x.DeleteAsync(categoryId), Times.Once);

    }

    [Fact]
    public async Task DeleteCategoryAsync_WithValidId_ShouldCallRepositoryDelete()
    {
        // Arrange
        var mockRepository = new Mock<ICategoryRepository>();
        var service = new CategoryService(mockRepository.Object);
        int categoryId = 1;
        // Act
        await service.DeleteCategoryAsync(categoryId);

        // Assert
        mockRepository.Verify(r => r.DeleteAsync(categoryId), Times.Once);
    }

    [Fact]
    public async Task GetPagedCategoriesAsync_WithValidRequest_ShouldReturnPagedResult()
    {
        //Arrange
        var mockRepository = new Mock<ICategoryRepository>();
        var service = new CategoryService(mockRepository.Object);
        List<Category> testCategories =
        [
            Category.Create(new CategorySaveRequest() { Name = "First" }),
            Category.Create(new CategorySaveRequest() { Name = "Second" }),
            Category.Create(new CategorySaveRequest() { Name = "Third" }),
        ];

        var pagedResult = new PagedResult<Category>
        {
            PageNumber = 1,
            PageSize = 2,
            TotalCount = 100,
            Items = testCategories,
        };

        var paginationRequest = new PaginationRequest
        {
            PageNumber = 1,
            PageSize = 10
        };
        mockRepository.Setup(x => x.GetPagedAsync(paginationRequest)).ReturnsAsync(pagedResult);

        //Act
        var resultCategories = await service.GetPagedCategoriesAsync(paginationRequest);

        //Assert
        resultCategories.Should().NotBeNull();
        resultCategories.Should().Be(pagedResult);
    }
   
}