using System.Text.Json;
using Domain.ModelClasses;

namespace FileData.DataAccess;

public class FileContext
{
    private string forumFilePath = "forum.json";
    
    private RedditForum? redditForum;
    
    public FileContext()
    {
        //Here we check if there is already a file at the given path.
        if (File.Exists(forumFilePath))
        {
            //Seed(); //If there's no file, we call the Seed()method(its purpose is to insert dummy data 
            LoadDataAsync();
        }
        else
        {
            CreateFileAsync();
        }
    }

    private async Task CreateFileAsync()
    {
        redditForum = new RedditForum();
        await SaveChangesAsync();
    }

    public RedditForum RedditForum
    {
        get
        {
            if (redditForum == null)
            {
                Task.FromResult(LoadDataAsync()); //Task.FromResult awaits the result when you cant use await
            }

            return redditForum!;
        }
    }
    
    /*private void Seed()
    {
        Post[] ts =
        {
            new Post("News")
            {
                Id = 1,
            },
            new Post("About cats")
            {
                Id = 2,
            },
            new Post("About dogs")
            {
                Id = 3,
            },
            new Post("Interesting facts")
            {
                Id = 4,
            },
            new Post("Programming tutorials")
            {
                Id = 5,
            },
        };
        redditForum.Posts = ts.ToList();
        SaveChanges();
    }*/

    private async Task LoadDataAsync()
    {
        string content = await File.ReadAllTextAsync(forumFilePath);
        redditForum = JsonSerializer.Deserialize<RedditForum>(content);
    }
    
    public async Task SaveChangesAsync()
    {
        string serialize = JsonSerializer.Serialize(RedditForum, new JsonSerializerOptions()
        {
            WriteIndented = true
        });
        await File.WriteAllTextAsync(forumFilePath, serialize);
        //redditForum = null;
    }
}

/*using System.Text.Json;
using Entities.Models;

namespace JsonDataAccess.Context;

public class JsonContext
{
    private string forumPath = "forum.json";

    private ForumContainer? forum;
    
    public ForumContainer Forum
    {
        get
        {
            if (forum == null)
            {
                LoadData();
            }

            return forum!;
        }
        private set{}
    }

    public JsonContext()
    {
        if (File.Exists(forumPath))
        {
            LoadData();
        }
        else
        {
            CreateFile();
        }
    }

    private void CreateFile()
    {
        forum = new ForumContainer();
        Task.FromResult(SaveChangesAsync());
    }

    private void LoadData()
    {
        string forumAsJson = File.ReadAllText(forumPath);
        forum = JsonSerializer.Deserialize<ForumContainer>(forumAsJson)!;
    }

    public async Task SaveChangesAsync()
    {
        string forumAsJson = JsonSerializer.Serialize(forum, new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = false
        });
        await File.WriteAllTextAsync(forumPath,forumAsJson);
        forum = null;
    }
}*/