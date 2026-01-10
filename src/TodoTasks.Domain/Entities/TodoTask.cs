using TodoTasks.Domain.ValueObjects;

namespace TodoTasks.Domain.Entities;

public class TodoTask : Entity
{
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public int AssignedTo { get; private set; }
    public DateTime? ReminderAt { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime? DueDate { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public int CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;

    private TodoTask() { } // For EF Core

    private TodoTask(TodoTaskCreateRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            throw new ArgumentException("Title cannot be empty", nameof(request.Title));

        if (request.Title.Length > 50)
            throw new ArgumentException("Title cannot exceed 50 characters", nameof(request.Title));

        if (request.Description?.Length > 500)
            throw new ArgumentException("Description cannot exceed 500 characters", nameof(request.Description));

        Title = request.Title.Trim();
        Description = request.Description?.Trim();
        DueDate = request.DueDate;
        AssignedTo = request.AssignedTo ?? 0;
        ReminderAt = request.ReminderAt;
        CategoryId = request.CategoryId ?? 0;
        IsCompleted = false;
    }

    public static TodoTask Create(TodoTaskCreateRequest request)
    {
        return new TodoTask(request);
    }

    public void Complete()
    {
        if (IsCompleted)
            throw new InvalidOperationException("Task is already completed");

        IsCompleted = true;
        CompletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Update(TodoTaskUpdateRequest request)
    {
        if (request.HasTitle)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
                throw new ArgumentException("Title cannot be empty", nameof(request.Title));

            if (request.Title.Length > 50)
                throw new ArgumentException("Title cannot exceed 50 characters", nameof(request.Title));

            Title = request.Title.Trim();
        }

        if (request.HasDescription && request.Description?.Length > 500)
            throw new ArgumentException(message: "Description cannot exceed 500 characters", nameof(request.Description));

        if (request.HasDescription)
            Description = request.Description?.Trim();

        if (request.HasAssignedTo)
            AssignedTo = request.AssignedTo!.Value;

        if (request.HasCategoryId)
            CategoryId = request.CategoryId!.Value;

        if (request.HasReminderAt)
            ReminderAt = request.ReminderAt;

        if (request.HasDueDate)
            DueDate = request.DueDate;

        UpdatedAt = DateTime.UtcNow;
    }

    public bool IsOverdue => DueDate.HasValue && !IsCompleted && DateTime.UtcNow > DueDate.Value;
}