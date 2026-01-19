using Xunit;
using FluentAssertions;
using TodoTasks.Domain.ValueObjects;
using TodoTasks.Domain.Entities;


namespace TodoTasks.Domain.Tests.Entities;

public class CategoryTests
{
    [Fact]
    public void Create_WithValidRequest_ShouldCreateCategorySuccessful()
    {
        //Arrange
        var request = new CategorySaveRequest
        {
            Name = "Unit Test Category",
            Color = Enums.TaskColorEnum.Yellow,
            Description = "Unit test Category Description"
        };

        //Act
        var category = Category.Create(request);

        //Assert
        category.Name.Should().Be("Unit Test Category");
        category.Color.Should().Be(Enums.TaskColorEnum.Yellow);
        category.Description.Should().Be("Unit test Category Description");
    }


    [Fact]
    public void Create_WithBlankName_ShouldThrowNullArgumentException()
    {
        //Arrange
        var categorySaveRequest = new CategorySaveRequest
        {
            Color = Enums.TaskColorEnum.Green,
            Description = "Category description"
        };

        //Act
        var act = () => Category.Create(categorySaveRequest);

        //Assert
        act.Should().Throw<ArgumentException>().WithMessage("Category name cannot be empty *")
            .And.ParamName.Should().Be("Name");
    }

    [Fact]
    public void Update_WithValidRequest_ShouldUpdateCategory()
    {
        var createRequest = new CategorySaveRequest
        {
            Name = "Original Name",
            Color = Enums.TaskColorEnum.Green,
            Description = "Original Description"
        };
        var category = Category.Create(createRequest);
        var originalUpdatedAt = category.UpdatedAt;

        var updateRequest = new CategoryUpdateRequest
        {
            Name = "Updated Category Name",
            Color = Enums.TaskColorEnum.White,
            Description = "Updated Description"
        };

        category.Update(updateRequest);

        category.Name.Should().Be("Updated Category Name");
        category.Color.Should().Be(Enums.TaskColorEnum.White);
        category.Description.Should().Be("Updated Description");
        category.UpdatedAt.Should().BeAfter(originalUpdatedAt ?? DateTime.MinValue);
    }

    [Fact]
    public void Update_WithNameOnly_ShouldUpdateOnlyName()
    {
        var createRequest = new CategorySaveRequest
        {
            Name = "Original Name",
            Color = Enums.TaskColorEnum.Green,
            Description = "Original Description"
        };
        var category = Category.Create(createRequest);

        var updateRequest = new CategoryUpdateRequest
        {
            Name = "New Name"
        };

        category.Update(updateRequest);

        category.Name.Should().Be("New Name");
        category.Color.Should().Be(Enums.TaskColorEnum.Green);
        category.Description.Should().Be("Original Description");
    }

    [Fact]
    public void Update_WithEmptyName_ShouldThrowArgumentException()
    {
        var category = Category.Create(new CategorySaveRequest { Name = "Test", Color = Enums.TaskColorEnum.Green });
        var updateRequest = new CategoryUpdateRequest { Name = "" };

        var act = () => category.Update(updateRequest);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Category name cannot be empty*")
            .And.ParamName.Should().Be("Name");
    }

    [Fact]
    public void Update_WithNameTooLong_ShouldThrowArgumentException()
    {
        var category = Category.Create(new CategorySaveRequest { Name = "Test", Color = Enums.TaskColorEnum.Green });
        var updateRequest = new CategoryUpdateRequest { Name = new string('a', 31) };

        var act = () => category.Update(updateRequest);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Category name cannot exceed 30 characters*")
            .And.ParamName.Should().Be("Name");
    }

    [Fact]
    public void Update_WithWhitespaceName_ShouldTrimName()
    {
        var category = Category.Create(new CategorySaveRequest { Name = "Test", Color = Enums.TaskColorEnum.Green });
        var updateRequest = new CategoryUpdateRequest { Name = "  Trimmed Name  " };

        category.Update(updateRequest);

        category.Name.Should().Be("Trimmed Name");
    }

    [Fact]
    public void Update_WithBlankName_ShouldThrowException()
    {
        var saveRequest = new CategorySaveRequest()
        {
            Color = Enums.TaskColorEnum.Red,
            Description = "Test descripiton",
            Name = "Test Category Name"
        };

        var category = Category.Create(saveRequest);

        var updateRequest = new CategoryUpdateRequest
        {
            Color = Enums.TaskColorEnum.Green,
            Description = "This is description"
        };

        var act = () => category.Update(updateRequest);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Category name cannot be empty*")
            .And.ParamName.Should().Be("Name");
    }



    [Fact]
    public void Update_WithNullDescription_ShouldSetDescriptionToNull()
    {
        var category = Category.Create(new CategorySaveRequest { Name = "Test", Color = Enums.TaskColorEnum.Green, Description = "Original" });
        var updateRequest = new CategoryUpdateRequest { Description = null };

        category.Update(updateRequest);

        category.Description.Should().BeNull();
    }

}