using System.Security.Claims;
using System.Text.Json;
using Domain.DataAccessContracts;
using Domain.ModelClasses;
using Microsoft.JSInterop;

namespace BlazorAppReddit.Authentication;

public class AuthServiceImplement : IAuthService
{
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;
    private readonly IUserDao userDao;
    private readonly IJSRuntime jsRuntime;
   

    public AuthServiceImplement(IUserDao userDao, IJSRuntime jsRuntime)
    {
        this.userDao = userDao;
        this.jsRuntime = jsRuntime;
    }
    
    public async Task LoginAsync(User user) // change signature in interface
    {
        // use method from dao object to check that the user you get is in the file
        if (await userDao.TryLogin(user))
        {
            ClaimsPrincipal principal = CreateClaimsPrincipal(user); // convert User object to ClaimsPrincipal
            OnAuthStateChanged.Invoke(principal);
        }
    }

    public async Task LogoutAsync()
    {
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
        OnAuthStateChanged.Invoke(claimsPrincipal);
    }

    public async Task<ClaimsPrincipal> GetAuthAsync()
    {
        User? user =  await GetUserFromCacheAsync(); // retrieve cached user, if any

        ClaimsPrincipal principal = CreateClaimsPrincipal(user); // create ClaimsPrincipal

        return principal;
    }
    
    private async Task<User?> GetUserFromCacheAsync()
    {
        string userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
        if (string.IsNullOrEmpty(userAsJson)) return null;
        User user = JsonSerializer.Deserialize<User>(userAsJson)!;
        return user;
    }

    

    private static ClaimsPrincipal CreateClaimsPrincipal(User? user) 
    { // you can make it so you give different roles to the user
        
        if (user != null)
        {
            ClaimsIdentity identity = ConvertUserToClaimsIdentity(user);
            return new ClaimsPrincipal(identity);
        }

        return new ClaimsPrincipal();
    }
    

    private async Task ClearUserFromCacheAsync()
    {
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
    }

    private static ClaimsIdentity ConvertUserToClaimsIdentity(User user)
    {
        // here we take the information of the User object and convert to Claims
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name, user.UserName),
          //  new Claim("Role", user.Role),
          //  new Claim("SecurityLevel", user.SecurityLevel.ToString()),
         //   new Claim("BirthYear", user.BirthYear.ToString()),
         //   new Claim("Domain", user.Domain)
        };

        return new ClaimsIdentity(claims, "apiauth_type");
    }
    
    
    
    
  
    
}