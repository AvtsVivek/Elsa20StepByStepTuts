using NodaTime;

namespace P20590ForkBranch.Models
{
    public class Comment
    {
        public string Author { get; set; } = default!;
        public Instant Timestamp { get; set; }
        public string Text { get; set; } = default!;
    }
}