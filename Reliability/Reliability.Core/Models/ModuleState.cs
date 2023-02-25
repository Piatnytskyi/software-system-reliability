using Reliability.Core.Enums;
using System.Xml.Serialization;

namespace Reliability.Core.Models
{
    public struct ModuleState : ICloneable, IEquatable<ModuleState>
    {
        public ModuleType Type { get; }
        public ModuleStatus Status { get; private set; }

        [XmlAttribute("irestorable")]
        public bool InfinitelyRestorable { get => AvailableRestoresCount == int.MaxValue; }
        public int AvailableRestoresCount { get; private set; } = int.MaxValue;

        public ModuleState(ModuleType moduleType, ModuleStatus status)
        {
            Type = moduleType;
            Status = status;
        }

        public ModuleState(ModuleType moduleType, int availableRestoresCount, ModuleStatus status)
        {
            Type = moduleType;
            AvailableRestoresCount = availableRestoresCount;
            Status = status;
        }

        public void SetModuleStatus(ModuleStatus status)
        {
            if (!InfinitelyRestorable && Status == ModuleStatus.Failed && status == ModuleStatus.Functional)
                AvailableRestoresCount = AvailableRestoresCount--;

            Status = status;
        }

        public object Clone()
        {
            return new ModuleState(Type, AvailableRestoresCount, Status);
        }

        public bool Equals(ModuleState other)
        {
            return Type == other.Type
                && Status == other.Status
                && InfinitelyRestorable == other.InfinitelyRestorable
                && AvailableRestoresCount == other.AvailableRestoresCount;
        }

        public override string ToString()
        {
            return $"{(int)Status}";
        }
    }
}
