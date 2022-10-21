using DataAccessExtensions.Contract;

namespace DataAccess.Entities;

public class Blog : IPkEntity<Guid>, ITracksChanges
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Tags { get; set; }
    public Guid CreatedById { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public Guid UpdatedById { get; set; }
    public DateTime UpdatedOnUtc { get; set; }
    
    public List<BlogPost>? BlogPosts { get; set; }
    public List<User?>? Authors { get; set; }
}