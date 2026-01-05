using TodoTasks.Domain.ValueObjects;

namespace TodoTasks.Domain.Entities;

public class Category : Entity
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string Color { get; private set; } = "#000000";

    private Category() { } // For EF Core

    public Category(string name, string? description = null, string color = "#000000")
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty", nameof(name));
        
        if (name.Length > 30)
            throw new ArgumentException("Category name cannot exceed 30 characters", nameof(name));
        
        Name = name.Trim();
        Description = description?.Trim();
        Color = color;
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

        SetUpdatedAt();
    }
}