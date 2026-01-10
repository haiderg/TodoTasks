using TodoTasks.Domain.Enums;

public record CategorySaveRequest
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public TaskColorEnum Color { get; set; }
}