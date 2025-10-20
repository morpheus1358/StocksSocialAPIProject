namespace ScriptureExplorer.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public int VerseId { get; set; }
        public Verse Verse { get; set; } = null!;
        public string UserId { get; set; } = string.Empty; // Simple user identifier for demo
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}