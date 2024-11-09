using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class JournalEntryController : ControllerBase
{
    private readonly MongoDBService _mongoDBService;

    public JournalEntryController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    // Create a new journal entry
    [HttpPost("addJournalEntry")]
    public async Task<IActionResult> AddJournalEntry([FromBody] JournalEntry journalEntry)
    {
        await _mongoDBService.JournalEntries.InsertOneAsync(journalEntry);
        return Ok("Journal entry added successfully");
    }

    // Get journal entry by journalentryid
    [HttpGet("getJournalEntry/{journalentryid}")]
    public async Task<IActionResult> GetJournalEntry(string journalentryid)
    {
        var entry = await _mongoDBService.JournalEntries.Find(je => je.JournalEntryId == journalentryid).FirstOrDefaultAsync();
        return entry != null ? Ok(entry) : NotFound("Journal entry not found");
    }

    // Update journal entry by journalentryid
    [HttpPut("updateJournalEntry/{journalentryid}")]
    public async Task<IActionResult> UpdateJournalEntry(string journalentryid, [FromBody] JournalEntry updatedEntry)
    {
        var result = await _mongoDBService.JournalEntries.ReplaceOneAsync(je => je.JournalEntryId == journalentryid, updatedEntry);
        return result.ModifiedCount > 0 ? Ok("Journal entry updated") : NotFound("Journal entry not found");
    }

    // Delete journal entry by journalentryid
    [HttpDelete("deleteJournalEntry/{journalentryid}")]
    public async Task<IActionResult> DeleteJournalEntry(string journalentryid)
    {
        var result = await _mongoDBService.JournalEntries.DeleteOneAsync(je => je.JournalEntryId == journalentryid);
        return result.DeletedCount > 0 ? Ok("Journal entry deleted") : NotFound("Journal entry not found");
    }
}
