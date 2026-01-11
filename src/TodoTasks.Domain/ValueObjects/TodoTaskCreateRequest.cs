namespace TodoTasks.Domain.ValueObjects;

public record TodoTaskCreateRequest
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public int? AssignedTo { get; set; }
    public DateTime? ReminderAt { get; set; }
    public DateTime? DueDate { get; set; }
    public int? CategoryId { get; set; }

    internal bool HasTitle => !string.IsNullOrWhiteSpace(Title);
}