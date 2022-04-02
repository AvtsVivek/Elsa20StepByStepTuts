namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Messages
{
    public class Greeting
    {
        public string From { get; set; } = default!;
        public string To { get; set; } = default!;
        public string Message { get; set; } = default!;
        public override string ToString() => $"{From} says \"{Message}\" to {To}.";
    }
}