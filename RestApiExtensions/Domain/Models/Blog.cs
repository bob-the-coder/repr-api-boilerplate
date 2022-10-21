namespace Domain.Models;

public class Blog
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Tags { get; set; }
    public Guid CreatedById { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public Guid UpdatedById { get; set; }
    public DateTime UpdatedOnUtc { get; set; }
    
    public List<User?>? Authors { get; set; }
    public List<BlogPost>? BlogPosts { get; set; }
}