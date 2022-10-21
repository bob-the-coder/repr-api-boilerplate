using System.Net;
using FastEndpoints;
using ServiceContracts.Services;

namespace ApiExample.Blog;

public class PostBlogRequest
{
    public string? Name { get; set; }
    public string[]? Tags { get; set; }

    public Domain.Models.Blog ToDomainModel()
    {
        return new Domain.Models.Blog()
        {
            Name = Name,
            Tags = Tags is not null ? string.Join(',', Tags.Select(tag => tag.Trim())) : string.Empty
        };
    }
} 

public class PostBlogResponse : PutBlogRequest
{
    public Guid CreatedBy { get; set; }
    public DateTime? CreatedOnUtc { get; set; }
    public Guid UpdatedBy { get; set; }
    public DateTime? UpdatedOnUtc { get; set; }

    public PostBlogResponse()
    {
    }

    public PostBlogResponse(Domain.Models.Blog blog)
    {
        Id = blog.Id;
        Name = blog.Name;
        Tags = blog.Tags?.Split(',');
        CreatedBy = blog.CreatedById;
        CreatedOnUtc = blog.CreatedOnUtc;
        UpdatedBy = blog.UpdatedById;
        UpdatedOnUtc = blog.UpdatedOnUtc;
    }
}


public class PostBlog : Endpoint<PostBlogRequest, PostBlogResponse>
{
    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("blog");
        AllowAnonymous();
    }

    public override Task HandleAsync(PostBlogRequest request, CancellationToken cancellationToken)
    {
        var domainModel = request.ToDomainModel();
        var service = Resolve<IBlogService>();

        var exists = service.Get(domainModel.Id);
        var createOrUpdate = service.Create(domainModel);

        if (!createOrUpdate.Success || createOrUpdate.Result is null)
        {
            return SendAsync(null!, (int)HttpStatusCode.InternalServerError, cancellationToken);
        }

        var response = new PostBlogResponse(createOrUpdate.Result);
        return SendAsync(response, cancellation: cancellationToken);
    }
}