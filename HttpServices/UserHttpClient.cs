using System.Text;
using System.Text.Json;
using Domain.DataAccessContracts;
using Domain.ModelClasses;

namespace HttpServices;

public class UserHttpClient : IUserDao
{
    private string HOST = "https://localhost:7221/";
    public async Task<bool> TryLogin(User user)
    {
        using HttpClient client = new ();
        string userAsJson = JsonSerializer.Serialize(user);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync(HOST+"User/Login", content);
        string reponseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {reponseContent}");
        }

        bool result = JsonSerializer.Deserialize<bool>(reponseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return result;
    }

    public async Task<User> AddUserAsync(User user)
    {
        using HttpClient client = new();

        string userAsJson = JsonSerializer.Serialize(user);

        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync(HOST+"User/AddUser", content);
        string responseContent = await response.Content.ReadAsStringAsync();
    
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {responseContent}");
        }
    
        User returned = JsonSerializer.Deserialize<User>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    
        return returned;
    }
    
    // this method is never used in this project
    public Task<User> GetUserByUsername(string userName)
    {
        throw new NotImplementedException();
    }

    // this method is never used in this project
    public Task DeleteUserAsync(string userName)
    {
        throw new NotImplementedException();
    }

    // this method is never used in this project
    public Task UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    // this method is never used in this project
    public Task<User> GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}