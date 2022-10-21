﻿namespace Domain.Models;

public class Feature 
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid CreatedById { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public Guid UpdatedById { get; set; }
    public DateTime UpdatedOnUtc { get; set; }
}