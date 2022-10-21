using Domain.JsonModels;

namespace Domain.Models;

public class BlogPost
{
    public Guid Id { get; set; }
    public Guid BlogId { get; set; }
    public string? Title { get; set; }
    public ViewContentContainer? Content { get; set; }
    public Guid CreatedById { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public Guid UpdatedById { get; set; }
    public DateTime UpdatedOnUtc { get; set; }
    public DateTime? PublishedOnUtc { get; set; }
    
    public User? CreatedBy { get; set; }
    public User? UpdatedBy { get; set; }
}