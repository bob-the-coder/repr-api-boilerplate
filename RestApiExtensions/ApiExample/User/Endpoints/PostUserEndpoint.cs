using ApiExample.User.RequestModels;
using ApiExample.User.ResponseModels;
using FastEndpoints;
using ServiceContracts.Services;

namespace ApiExample.User.Endpoints;

public class PostUserEndpoint : Endpoint<PostUserRequest, UserResponse>
{
    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("user");
        AllowAnonymous();
    }
    
    public override Task HandleAsync(PostUserRequest request, CancellationToken cancellationToken)
    {
        
        var user = new Domain.Models.User
        {
            FullName = request.Name,
            Email = request.Email
        };

        var createUser = Resolve<IUserService>().Create(user);
        if (!createUser.Success)
        {
            return SendErrorsAsync(cancellation: cancellationToken);
        }

        var response = new UserResponse(createUser.Result!);

        return SendAsync(response, cancellation: cancellationToken);
    }
}