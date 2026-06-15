using Domain.DataAccessContracts;
using Domain.ModelClasses;
using FileData.DataAccess;


namespace FileData.DaoObjects;

public class PostDao : IPostDao
{
   private FileContext fileContext;

   public PostDao(FileContext fileContext)
   {
      this.fileContext = fileContext;
   }
   
   

   public async Task<Post> GetPostByIdAsync(int id)
   {
      return fileContext.RedditForum.Posts.First(t => t.Id == id);
   }
   

   public async Task DeletePostAsync(int id)
   {
      var firstOrDefault = fileContext.RedditForum.Posts.FirstOrDefault(p => p.Id == id);
      if (firstOrDefault != null)
      {
         fileContext.RedditForum.Posts.Remove(firstOrDefault);
      }
      await fileContext.SaveChangesAsync();
   }
   

   public async Task<ICollection<Post>> GetAllPostsAsync()
   {
      ICollection<Post> posts = fileContext.RedditForum.Posts; // get RedditForum, get the lists of posts from ForumReddit
      return posts;
   }
   

   public async Task<Post> AddPostAsync(Post post)
   {
      int largestId = 0;
      if (fileContext.RedditForum.Posts.Any())
      {
         largestId = fileContext.RedditForum.Posts.Max(t => t.Id);
      }
      int nextId = largestId + 1;
      post.Id = nextId;
      fileContext.RedditForum.Posts.Add(post);
      await fileContext.SaveChangesAsync();
      return post;
   }
   

   public async Task UpdatePostAsync(Post post)
   {
     var updatePost = fileContext.RedditForum.Posts.First(t => t.Id == post.Id);
     updatePost = post;
     await fileContext.SaveChangesAsync();
   }
   
   
   
   // we need the adapters to let the Blazor app get data from the FileContext. These are our Data Access Objects
   //We currently have 2 type of domain object, so we just need 2 DAO to provide CRUD operations on Users and Posts
   //This class should implement the IPostDao interface from the Domain component, and provide implementations for the methods
}