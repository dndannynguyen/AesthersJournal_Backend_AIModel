using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class UserStatusController : ControllerBase
{
    private readonly MongoDBService _mongoDBService;

    public UserStatusController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    // Create a new user status
    [HttpPost("addUserStatus")]
    public async Task<IActionResult> AddUserStatus([FromBody] UserStatus userStatus)
    {
        await _mongoDBService.UserStatus.InsertOneAsync(userStatus);
        return Ok("User status added successfully");
    }

    // Get user status by userid
    [HttpGet("getUserStatus/{userid}")]
    public async Task<IActionResult> GetUserStatus(string userid)
    {
        var status = await _mongoDBService.UserStatus.Find(us => us.UserId == userid).FirstOrDefaultAsync();
        return status != null ? Ok(status) : NotFound("User status not found");
    }

    // Update user status by userid
    [HttpPut("updateUserStatus/{userid}")]
    public async Task<IActionResult> UpdateUserStatus(string userid, [FromBody] UserStatus updatedStatus)
    {
        var result = await _mongoDBService.UserStatus.ReplaceOneAsync(us => us.UserId == userid, updatedStatus);
        return result.ModifiedCount > 0 ? Ok("User status updated") : NotFound("User status not found");
    }

    // Delete user status by userid
    [HttpDelete("deleteUserStatus/{userid}")]
    public async Task<IActionResult> DeleteUserStatus(string userid)
    {
        var result = await _mongoDBService.UserStatus.DeleteOneAsync(us => us.UserId == userid);
        return result.DeletedCount > 0 ? Ok("User status deleted") : NotFound("User status not found");
    }
}
