using ApiExample.User.RequestModels;
using ApiExample.User.ResponseModels;
using FastEndpoints;
using ServiceContracts.Contract;
using ServiceContracts.Services;

namespace ApiExample.User.Endpoints;

public class GetUserEndpoint : Endpoint<GetUserRequest, UserResponse>
{
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("user/{id}");
        AllowAnonymous();
    }
    
    /// <summary>
    /// Get a user by Id
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task HandleAsync(GetUserRequest request, CancellationToken cancellationToken)
    {
        if (request.Id == Constants.Steve.Id)
            return SendAsync(new UserResponse(Constants.Steve), cancellation: cancellationToken);
        
        var userSerivce = Resolve<IUserService>();

        var getUser = userSerivce.Get(request.Id);
        if (!getUser.Success)
        {
            return SendNotFoundAsync(cancellationToken);
        }

        var response = new UserResponse(getUser.Result!);

        return SendAsync(response, cancellation: cancellationToken);
    }
}