using Domain.DataAccessContracts;
using Domain.ModelClasses;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLogicWithRestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private IPostDao _postDao;

    public PostController(IPostDao postDao)
    {
        _postDao = postDao;
    }
    
    /* The counterpart methods below, taken from IUserDao are not implemented here, as they are never used:
            - public Task UpdatePostAsync(Post post);
            - public Task DeletePostAsync(int id);
     */
    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPostByIdAsync(int id)
    {
        try
        {
            Post post = await _postDao.GetPostByIdAsync(id);
            return Ok(post);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Post>>> GetAllPostsAsync() // this one returns a List of posts
    {
        try
        {
            Thread.Sleep(2000);
            ICollection<Post> posts = await _postDao.GetAllPostsAsync();
            return Ok(posts);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Post>> AddPostAsync([FromBody] Post post)
    {
        try
        {
            Post postAdded = await _postDao.AddPostAsync(post);
            Thread.Sleep(1000);
            return Created($"/post/{postAdded.Id}", postAdded);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
}