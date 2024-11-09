using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

public class JournalEntry
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("journalentryid")]
    public string JournalEntryId { get; set; }

    [BsonElement("title")]
    public string Title { get; set; }

    [BsonElement("date")]
    public DateTime Date { get; set; } = DateTime.Now;

    [BsonElement("content")]
    public string Content { get; set; }

    [BsonElement("summary")]
    public string Summary { get; set; }

    [BsonElement("chatid")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ChatId { get; set; }
}
