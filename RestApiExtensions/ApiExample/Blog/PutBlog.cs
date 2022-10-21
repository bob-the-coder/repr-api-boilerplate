using System.Net;
using FastEndpoints;
using ServiceContracts.Services;

namespace ApiExample.Blog;

public class PutBlogRequest : PostBlogRequest
{
    public Guid Id { get; set; }

    public new Domain.Models.Blog ToDomainModel()
    {
        return new Domain.Models.Blog()
        {
            Id = Id,
            Name = Name,
            Tags = Tags is not null ? string.Join(',', Tags.Select(tag => tag.Trim())) : string.Empty
        };
    }
}


public class PutBlog : Endpoint<PutBlogRequest, PostBlogResponse>
{
    public override void Configure()
    {
        Verbs(Http.PUT);
        Routes("blog");
        AllowAnonymous();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task HandleAsync(PutBlogRequest request, CancellationToken cancellationToken)
    {
        var domainModel = request.ToDomainModel();
        var service = Resolve<IBlogService>();

        var exists = service.Get(domainModel.Id);
        var createOrUpdate = service.Update(domainModel);

        if (!createOrUpdate.Success || createOrUpdate.Result is null)
        {
            return SendAsync(null!, (int)HttpStatusCode.InternalServerError, cancellationToken);
        }

        var response = new PostBlogResponse(createOrUpdate.Result);
        return SendAsync(response, cancellation: cancellationToken);
    }
}