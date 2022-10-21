namespace DataAccessExtensions.Contract;

public interface ITracksChanges
{
    Guid CreatedById { get; set; }
    DateTime CreatedOnUtc { get; set; }
    Guid UpdatedById { get; set; }
    DateTime UpdatedOnUtc { get; set; }
}