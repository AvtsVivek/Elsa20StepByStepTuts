using NodaTime;

//P20596ForkBranchWithTimerAndHttp
namespace P20596ForkBranchWithTimerAndHttp.Models
{
    public class Comment
    {
        public string Author { get; set; } = default!;
        public Instant Timestamp { get; set; }
        public string Text { get; set; } = default!;
    }
}