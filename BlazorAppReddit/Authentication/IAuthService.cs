using System.Security.Claims;
using Domain.ModelClasses;

namespace BlazorAppReddit.Authentication;

public interface IAuthService
{
    public Task LoginAsync(User user);
    public Task LogoutAsync();
    public Task<ClaimsPrincipal> GetAuthAsync();
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } // property

}