using Domain.DataAccessContracts;
using Domain.ModelClasses;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLogicWithRestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    /* The counterpart methods below, taken from IUserDao are not implemented here, as they are never used 
            - public Task DeleteUserAsync(string userName);
            - public Task UpdateUserAsync(User user);
            - public Task<User> GetUserByIdAsync(int id);
    */
    private IUserDao _userDao;

    public UserController(IUserDao userDao)
    {
        _userDao = userDao;
    }

    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<bool>> TryLogin([FromBody] User user) 
    {
        try
        {
            User? userTemp = await _userDao.GetUserByUsername(user.UserName);
            if (userTemp != null && userTemp.Password.Equals(user.Password)) return Ok(true);
            return Ok(false);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    [Route("AddUser")]
    public async Task<ActionResult<User>> AddUserAsync(User user)
    {
        try
        {
            User? userTemp = await _userDao.GetUserByUsername(user.UserName);
            if (userTemp == null)
            {
                userTemp = await _userDao.AddUserAsync(user);
                if (userTemp != null) return Created($"/",userTemp);
            }
            throw new HttpRequestException("The user name is already taken");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
}