using Reliability.Core.Enums;

namespace Reliability.Core.Models
{
    public struct ChangeTag
    {
        public ChangeType Type { get; set; }
        public decimal Intensity { get; set; }

        public ChangeTag(ChangeType type, decimal intensity)
        {
            Type = type;
            Intensity = intensity;
        }

        public override string ToString()
        {
            return $"{Type}";
        }
    }
}
