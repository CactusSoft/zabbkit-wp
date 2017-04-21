using System.ComponentModel;

namespace CactusSoft.Stierlitz.Domain
{
    public enum TriggerPriority
    {
        [Description("Not classified")]
        NotClassified = 0,

        [Description("Information")]
        Information = 1,

        [Description("Warning")]
        Warning = 2,

        [Description("Average")]
        Average = 3,

        [Description("High")]
        High = 4,

        [Description("Disaster")]
        Disaster = 5,
    }
}
