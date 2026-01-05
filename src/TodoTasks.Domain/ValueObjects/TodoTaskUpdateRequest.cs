namespace TodoTasks.Domain.ValueObjects;

public class TodoTaskUpdateRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? AssignedTo { get; set; }
    public int? CategoryId { get; set; }
    public DateTime? ReminderAt { get; set; }
    public DateTime? DueDate { get; set; }

    public bool HasTitle => !string.IsNullOrEmpty(Title);
    public bool HasDescription => Description != null;
    public bool HasAssignedTo => AssignedTo.HasValue;
    public bool HasCategoryId => CategoryId.HasValue;
    public bool HasReminderAt => ReminderAt.HasValue;
    public bool HasDueDate => DueDate.HasValue;
}