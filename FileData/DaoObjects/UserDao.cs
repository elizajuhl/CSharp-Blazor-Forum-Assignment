using System.Security.Cryptography.X509Certificates;
using Domain.DataAccessContracts;
using Domain.ModelClasses;
using FileData.DataAccess;

namespace FileData.DaoObjects;

public class UserDao : IUserDao
{
    private FileContext fileContext;

    public UserDao(FileContext fileContext)
    {
        this.fileContext = fileContext;
    }


    public async Task<bool> TryLogin(User user)
    {
        User? userLogin = null;
        
        userLogin = fileContext.RedditForum.Users.FirstOrDefault(p =>
            p.UserName.Equals(user.UserName) && p.Password.Equals(user.Password));

        return userLogin != null; // if doesn't find something matching will return that userLogin is null
                                  // (so the method will return true or false)

    }

    public async Task<User> AddUserAsync(User user)
    {
        int largestId = 0;
        if (fileContext.RedditForum.Users.Count != 0)
        {
            largestId = fileContext.RedditForum.Users.Max(t => t.Id);
        }

        int nextId = largestId + 1;
        user.Id = nextId;
        fileContext.RedditForum.Users.Add(user);
        await fileContext.SaveChangesAsync();
        return user;
    }

    public async Task<User> GetUserByUsername(string userName)
    {
        User? user = fileContext.RedditForum.Users.FirstOrDefault(u => u.UserName.Equals(userName));
        if (user != null) return user;
        return null;
    }

    public async Task DeleteUserAsync(string userName)
    {
        var firstOrDefault = fileContext.RedditForum.Users.FirstOrDefault(p => p.UserName.Equals(userName));
        if (firstOrDefault != null)
        {
            fileContext.RedditForum.Users.Remove(firstOrDefault);
        }

        await fileContext.SaveChangesAsync();
    }


    public async Task UpdateUserAsync(User user)
    {
        var updateUser = fileContext.RedditForum.Users.First(t => t.Id == user.Id);
        updateUser = user;
        await fileContext.SaveChangesAsync();
    }


    public async Task<User> GetUserByIdAsync(int id)
    {
        return fileContext.RedditForum.Users.First(t => t.Id == id);
    }
}