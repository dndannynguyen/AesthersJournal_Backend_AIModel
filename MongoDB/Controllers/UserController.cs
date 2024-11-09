using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly MongoDBService _mongoDBService;

    public UserController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    // Create a new user
    [HttpPost("addUser")]
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        await _mongoDBService.Users.InsertOneAsync(user);
        return Ok("User added successfully");
    }

    // Get a user by userid
    [HttpGet("getUser/{userid}")]
    public async Task<IActionResult> GetUser(string userid)
    {
        var user = await _mongoDBService.Users.Find(u => u.UserId == userid).FirstOrDefaultAsync();
        return user != null ? Ok(user) : NotFound("User not found");
    }

    // Update a user by userid
    [HttpPut("updateUser/{userid}")]
    public async Task<IActionResult> UpdateUser(string userid, [FromBody] User updatedUser)
    {
        var result = await _mongoDBService.Users.ReplaceOneAsync(u => u.UserId == userid, updatedUser);
        return result.ModifiedCount > 0 ? Ok("User updated") : NotFound("User not found");
    }

    // Delete a user by userid
    [HttpDelete("deleteUser/{userid}")]
    public async Task<IActionResult> DeleteUser(string userid)
    {
        var result = await _mongoDBService.Users.DeleteOneAsync(u => u.UserId == userid);
        return result.DeletedCount > 0 ? Ok("User deleted") : NotFound("User not found");
    }
}
