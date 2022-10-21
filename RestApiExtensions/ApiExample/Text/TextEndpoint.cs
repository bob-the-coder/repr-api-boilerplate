using System.Net;
using System.Security.Claims;
using ApiExample.Blog;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ServiceContracts.Services;
using TextCompression;

namespace ApiExample.Text;

public record UserData(int Id, string Name);
public class CompressRequest
{
}

public record CompressResponse(UserData? Data);

public abstract class TextEndpoint : Endpoint<CompressRequest, CompressResponse>
{
    public override void Configure()
    {
        Routes("text/compress");
    }

    public override async Task HandleAsync(CompressRequest request, CancellationToken cancellationToken)
    {
        var data = GetUser();
        if (data is null)
        {
            await Login();
            data = GetUser();
        }
        
        await SendAsync(new CompressResponse(data), cancellation: cancellationToken);
    }
    
    private async Task Login()
    {
        var user = JsonConvert.SerializeObject(new UserData(213, "Bob"));
        var claims = new[]
        {
            new Claim(ClaimTypes.UserData, user)
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
        {
            IsPersistent = false
        });
    }

    private UserData? GetUser()
    {
        var user = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.UserData);
        if (user is null) return null;
        
        var data = JsonConvert.DeserializeObject<UserData>(user.Value);
        return data;
    }
}

public class PostText: TextEndpoint {
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.POST);
        AllowAnonymous();
    }
}


public class GetText: TextEndpoint {
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.GET);
        AllowAnonymous();
    }
}