namespace TodoTasks.Domain.ValueObjects;

public record TodoTaskUpdateRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? AssignedTo { get; set; }
    public int? CategoryId { get; set; }
    public DateTime? ReminderAt { get; set; }
    public DateTime? DueDate { get; set; }

    internal bool HasTitle => !string.IsNullOrEmpty(Title);
    internal bool HasDescription => Description != null;
    internal bool HasAssignedTo => AssignedTo.HasValue;
    internal bool HasCategoryId => CategoryId.HasValue;
    internal bool HasReminderAt => ReminderAt.HasValue;
    internal bool HasDueDate => DueDate.HasValue;
}