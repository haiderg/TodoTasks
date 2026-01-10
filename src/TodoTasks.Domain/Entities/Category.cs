using TodoTasks.Domain.ValueObjects;
using TodoTasks.Domain.Enums;

namespace TodoTasks.Domain.Entities;

public class Category : Entity
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public TaskColorEnum Color { get; private set; }

    private Category() { } // For EF Core

    private Category(CategorySaveRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ArgumentException("Category name cannot be empty", nameof(request.Name));
        
        if (request.Name.Length > 30)
            throw new ArgumentException("Category name cannot exceed 30 characters", nameof(request.Name));
        
        Name = request.Name.Trim();
        Description = request.Description?.Trim();
        Color = request.Color;
    }

    public void Update(CategoryUpdateRequest request)
    {
        if (request.HasName)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException(message: "Category name cannot be empty", nameof(request.Name));
            
            if (request.Name.Length > 30)
                throw new ArgumentException(message: "Category name cannot exceed 30 characters", nameof(request.Name));

            Name = request.Name.Trim();
        }

        if (request.HasDescription)
            Description = request.Description?.Trim();

        if (request.HasColor)
            Color = request.Color!;

        UpdatedAt = DateTime.UtcNow;
    }

    public static Category Create(CategorySaveRequest request)
    {
        return new Category(request);
    }
}