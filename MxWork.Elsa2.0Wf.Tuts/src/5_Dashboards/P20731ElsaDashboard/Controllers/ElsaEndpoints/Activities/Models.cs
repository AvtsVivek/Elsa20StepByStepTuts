using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P20731ElsaDashboard.Controllers.ElsaEndpoints.Activities
{
    public class ActivityDescriptor
    {

        public string? Type { get; set; }
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public ActivityTraits Traits { get; set; }
        public string[]? Outcomes { get; set; }
        public ActivityPropertyDescriptor[]? Properties { get; set; }
    }

    public class ActivityPropertyDescriptor
    {
        //public ActivityPropertyDescriptor();
        //public ActivityPropertyDescriptor(string name, Type type, string uiHint, string label, string? hint = null, object? options = null, string? category = null, object? defaultValue = null, string? defaultSyntax = "Literal", IEnumerable<string>? supportedSyntaxes = null);

        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? UIHint { get; set; }
        public string? Label { get; set; }
        public string? Hint { get; set; }
        public object? Options { get; set; }
        public string? Category { get; set; }
        public object? DefaultValue { get; set; }
        public string? DefaultSyntax { get; set; }
        public IList<string>? SupportedSyntaxes { get; set; }
    }
    public enum ActivityTraits
    {
        Action = 1,
        Trigger = 2,
        Job = 4
    }
}
