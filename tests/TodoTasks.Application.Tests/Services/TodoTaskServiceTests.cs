using Xunit;
using FluentAssertions;
using Moq;
using TodoTasks.Domain.Entities;
using TodoTasks.Domain.Repositories;
using TodoTasks.Domain.ValueObjects;
using TodoTasks.Domain.Enums;
using TodoTasks.Application.Services;

namespace TodoTasks.Application.Tests.Services;

public class TodoTaskServiceTests
{
    [Fact]
    public async Task CreateTaskAsync_WithValidRequest_ShouldCreateAndReturnTask()
    {
        // Arrange
        var mockRepository = new Mock<ITodoTaskRepository>();
        var service = new TodoTaskService(mockRepository.Object);
        var request = new TodoTaskCreateRequest
        {
            Title = "Test Task",
            Description = "Test Description",
            DueDate = DateTime.Now.AddDays(1),
            CategoryId = 1
        };

        mockRepository.Setup(r => r.AddAsync(It.IsAny<TodoTask>()))
            .ReturnsAsync((TodoTask t) => t);

        // Act
        var result = await service.CreateTaskAsync(request);

        // Assert
        result.Should().NotBeNull();
        mockRepository.Verify(r => r.AddAsync(It.Is<TodoTask>(
            t => t.Title == "Test Task" && t.Description == "Test Description")), Times.Once);
    }

    [Fact]
    public async Task GetTaskAsync_WithValidId_ShouldReturnTask()
    {
        // Arrange
        var mockRepository = new Mock<ITodoTaskRepository>();
        var service = new TodoTaskService(mockRepository.Object);
        var expectedTask = TodoTask.Create(new TodoTaskCreateRequest { Title = "Test" });
        
        mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(expectedTask);

        // Act
        var result = await service.GetTaskAsync(1);

        // Assert
        result.Should().Be(expectedTask);
        mockRepository.Verify(r => r.GetByIdAsync(1), Times.Once);
    }

    [Fact]
    public async Task GetTaskAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        var mockRepository = new Mock<ITodoTaskRepository>();
        var service = new TodoTaskService(mockRepository.Object);
        
        mockRepository.Setup(r => r.GetByIdAsync(-1)).ReturnsAsync((TodoTask?)null);

        // Act
        var result = await service.GetTaskAsync(-1);

        // Assert
        result.Should().BeNull();
        mockRepository.Verify(r => r.GetByIdAsync(-1), Times.Once);
    }

    [Fact]
    public async Task GetAllTasksAsync_WhenCalled_ShouldReturnAllTasks()
    {
        // Arrange
        var mockRepository = new Mock<ITodoTaskRepository>();
        var service = new TodoTaskService(mockRepository.Object);
        var tasks = new List<TodoTask>
        {
            TodoTask.Create(new TodoTaskCreateRequest { Title = "Task 1" }),
            TodoTask.Create(new TodoTaskCreateRequest { Title = "Task 2" }),
            TodoTask.Create(new TodoTaskCreateRequest { Title = "Task 3" })
        };

        mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(tasks);

        // Act
        var result = await service.GetAllTasksAsync();

        // Assert
        result.Count().Should().Be(3);
        mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetPagedTasksAsync_WithValidRequest_ShouldReturnPagedResult()
    {
        // Arrange
        var mockRepository = new Mock<ITodoTaskRepository>();
        var service = new TodoTaskService(mockRepository.Object);
        var pagedResult = new PagedResult<TodoTask>
        {
            PageNumber = 1,
            PageSize = 10,
            TotalCount = 50,
            Items = new List<TodoTask>
            {
                TodoTask.Create(new TodoTaskCreateRequest { Title = "Task 1" }),
                TodoTask.Create(new TodoTaskCreateRequest { Title = "Task 2" })
            }
        };
        var request = new PaginationRequest { PageNumber = 1, PageSize = 10 };

        mockRepository.Setup(r => r.GetPagedAsync(request)).ReturnsAsync(pagedResult);

        // Act
        var result = await service.GetPagedTasksAsync(request);

        // Assert
        result.Should().Be(pagedResult);
        mockRepository.Verify(r => r.GetPagedAsync(request), Times.Once);
    }

    [Fact]
    public async Task UpdateTaskAsync_WithValidId_ShouldUpdateTask()
    {
        // Arrange
        var mockRepository = new Mock<ITodoTaskRepository>();
        var service = new TodoTaskService(mockRepository.Object);
        var existingTask = TodoTask.Create(new TodoTaskCreateRequest { Title = "Old Title" });
        var updateRequest = new TodoTaskUpdateRequest { Title = "New Title", Description = "Updated" };

        mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingTask);

        // Act
        await service.UpdateTaskAsync(1, updateRequest);

        // Assert
        existingTask.Title.Should().Be("New Title");
        mockRepository.Verify(r => r.GetByIdAsync(1), Times.Once);
        mockRepository.Verify(r => r.UpdateAsync(existingTask), Times.Once);
    }

    [Fact]
    public async Task UpdateTaskAsync_WithInvalidId_ShouldThrowArgumentException()
    {
        // Arrange
        var mockRepository = new Mock<ITodoTaskRepository>();
        var service = new TodoTaskService(mockRepository.Object);
        var updateRequest = new TodoTaskUpdateRequest { Title = "New Title" };

        mockRepository.Setup(r => r.GetByIdAsync(-1)).ReturnsAsync((TodoTask?)null);

        // Act
        var act = () => service.UpdateTaskAsync(-1, updateRequest);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("Task not found");
        mockRepository.Verify(r => r.GetByIdAsync(-1), Times.Once);
        mockRepository.Verify(r => r.UpdateAsync(It.IsAny<TodoTask>()), Times.Never);
    }

    [Fact]
    public async Task CompleteTaskAsync_WithValidId_ShouldMarkTaskAsComplete()
    {
        // Arrange
        var mockRepository = new Mock<ITodoTaskRepository>();
        var service = new TodoTaskService(mockRepository.Object);
        var task = TodoTask.Create(new TodoTaskCreateRequest { Title = "Test Task" });

        mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(task);

        // Act
        await service.CompleteTaskAsync(1);

        // Assert
        task.IsCompleted.Should().BeTrue();
        mockRepository.Verify(r => r.GetByIdAsync(1), Times.Once);
        mockRepository.Verify(r => r.UpdateAsync(task), Times.Once);
    }

    [Fact]
    public async Task CompleteTaskAsync_WithInvalidId_ShouldThrowArgumentException()
    {
        // Arrange
        var mockRepository = new Mock<ITodoTaskRepository>();
        var service = new TodoTaskService(mockRepository.Object);

        mockRepository.Setup(r => r.GetByIdAsync(-1)).ReturnsAsync((TodoTask?)null);

        // Act
        var act = () => service.CompleteTaskAsync(-1);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("Task not found");
        mockRepository.Verify(r => r.GetByIdAsync(-1), Times.Once);
        mockRepository.Verify(r => r.UpdateAsync(It.IsAny<TodoTask>()), Times.Never);
    }

    [Fact]
    public async Task DeleteTaskAsync_WithValidId_ShouldCallRepositoryDelete()
    {
        // Arrange
        var mockRepository = new Mock<ITodoTaskRepository>();
        var service = new TodoTaskService(mockRepository.Object);

        // Act
        await service.DeleteTaskAsync(1);

        // Assert
        mockRepository.Verify(r => r.DeleteAsync(1), Times.Once);
    }
}
