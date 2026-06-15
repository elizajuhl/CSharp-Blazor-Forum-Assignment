namespace Domain.ModelClasses;

public class RedditForum
{
    public ICollection<User> Users { get; set; }
    public ICollection<Post> Posts { get; set; }

    public RedditForum()
    {
        Users = new List<User>();
        Posts = new List<Post>();
    }
}