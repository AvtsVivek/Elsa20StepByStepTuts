using Elsa.Activities.Entity.Attributes;

namespace P20150EntityChanged
{
    [EntityName("Test-Entity")]
    public class Entity
    {
        public string Id { get; set; } = default!;
        public string Title { get; set; } = default!;
    }
}
