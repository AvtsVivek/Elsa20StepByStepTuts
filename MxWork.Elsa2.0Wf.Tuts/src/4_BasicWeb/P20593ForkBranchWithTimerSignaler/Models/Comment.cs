using NodaTime;

//P20593ForkBranchWithTimerSignaler
namespace P20593ForkBranchWithTimerSignaler.Models
{
    public class Comment
    {
        public string Author { get; set; } = default!;
        public Instant Timestamp { get; set; }
        public string Text { get; set; } = default!;
    }
}