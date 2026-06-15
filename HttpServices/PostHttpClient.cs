using System.Text;
using System.Text.Json;
using Domain.DataAccessContracts;
using Domain.ModelClasses;

namespace HttpServices;

public class PostHttpClient : IPostDao
{
    private string HOST = "https://localhost:7221/";
    public async Task<ICollection<Post>> GetAllPostsAsync()
    {
        using HttpClient client = new ();
        HttpResponseMessage response = await client.GetAsync(HOST+"post");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        ICollection<Post> posts = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return posts;
    }

    public async Task<Post> GetPostByIdAsync(int id)
    {
        using HttpClient client = new ();
        HttpResponseMessage response = await client.GetAsync(HOST+$"post/{id}");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        Post post = JsonSerializer.Deserialize<Post>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return post;
    }
    
    public async Task<Post> AddPostAsync(Post post)
    {
        using HttpClient client = new();

        string postAsJson = JsonSerializer.Serialize(post);

        StringContent content = new(postAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync(HOST+"post", content);
        string responseContent = await response.Content.ReadAsStringAsync();
    
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {responseContent}");
        }
    
        Post returned = JsonSerializer.Deserialize<Post>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    
        return returned;
    }

    // this method is never used in this project
    public Task UpdatePostAsync(Post post)
    {
        throw new NotImplementedException();
    }

    // this method is never used in this project
    public Task DeletePostAsync(int id)
    {
        throw new NotImplementedException();
    }
}