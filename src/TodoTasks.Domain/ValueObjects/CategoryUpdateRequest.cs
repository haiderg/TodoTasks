using TodoTasks.Domain.Enums;

namespace TodoTasks.Domain.ValueObjects;

public record CategoryUpdateRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public TaskColorEnum Color { get; set; }

    public bool HasName => !string.IsNullOrEmpty(Name);
    public bool HasDescription => Description != null;
    public bool HasColor => Enum.IsDefined(typeof(TaskColorEnum), Color);
}