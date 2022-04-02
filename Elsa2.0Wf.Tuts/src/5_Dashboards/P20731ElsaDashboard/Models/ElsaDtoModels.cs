//using Elsa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P20731ElsaDashboard.Models
{
    public record WorkflowDefinitionSummaryModel(
    string Id,
    string DefinitionId,
    string? Name,
    string? DisplayName,
    string? Description,
    int Version,
    bool IsSingleton,
    WorkflowPersistenceBehavior PersistenceBehavior,
    bool IsPublished,
    bool IsLatest);

    public class WorkflowBlueprintSummaryModel
    {
        public string Id { get; set; } = default!;
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public int Version { get; set; }
        public string? TenantId { get; set; }
        public bool IsSingleton { get; set; }
        public bool IsPublished { get; set; }
        public bool IsLatest { get; set; }
    }
    public class WorkflowBlueprintModel : CompositeActivityBlueprintModel
    {
        //public WorkflowBlueprintModel();

        public int Version { get; set; }
        public string? TenantId { get; set; }
        public bool IsSingleton { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsPublished { get; set; }
        public bool IsLatest { get; set; }
        public Elsa.Models.Variables Variables { get; set; }
        public P20731ElsaDashboard.Models.WorkflowContextOptions? ContextOptions { get; set; }
        public P20731ElsaDashboard.Models.WorkflowPersistenceBehavior PersistenceBehavior { get; set; }
        public bool DeleteCompletedInstances { get; set; }
        public Elsa.Models.Variables CustomAttributes { get; set; }
    }

    public class CompositeActivityBlueprintModel : ActivityBlueprintModel
    {
        //public CompositeActivityBlueprintModel();

        public ICollection<ActivityBlueprintModel> Activities { get; set; }
        public ICollection<ConnectionModel> Connections { get; set; }
    }

    public class ConnectionModel
    {
        //public ConnectionModel();

        public string SourceActivityId { get; set; }
        public string TargetActivityId { get; set; }
        public string Outcome { get; set; }
    }

    public class ActivityBlueprintModel
    {
        //public ActivityBlueprintModel();

        public string Id { get; set; }
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public string Type { get; set; }
        public string? ParentId { get; set; }
        public bool PersistWorkflow { get; set; }
        public bool LoadWorkflowContext { get; set; }
        public bool SaveWorkflowContext { get; set; }
        public Elsa.Models.Variables Properties { get; set; }
    }

    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum WorkflowPersistenceBehavior
    {
        Suspended = 0,
        WorkflowBurst = 1,
        WorkflowPassCompleted = 2,
        ActivityExecuted = 3
    }
    public class WorkflowContextOptions
    {
        //public WorkflowContextOptions();

        [Newtonsoft.Json.JsonConverter(typeof(Elsa.Serialization.Converters.TypeJsonConverter))]
        [System.ComponentModel.TypeConverter(typeof(Elsa.Converters.TypeTypeConverter))]
        public Type? ContextType { get; set; }
        public WorkflowContextFidelity ContextFidelity { get; set; }
    }
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum WorkflowContextFidelity
    {
        Burst = 0,
        Activity = 1
    }
}
