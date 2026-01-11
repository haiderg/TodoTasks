using TodoTasks.Domain.Enums;

namespace TodoTasks.Domain.ValueObjects;

public record CategoryUpdateRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public TaskColorEnum Color { get; set; }

    internal bool HasName => !string.IsNullOrEmpty(Name);
    internal bool HasDescription => Description != null;
    internal bool HasColor => Enum.IsDefined(typeof(TaskColorEnum), Color);
}