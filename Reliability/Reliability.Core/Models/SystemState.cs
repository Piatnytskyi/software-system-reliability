using System.Collections.ObjectModel;
using System.Text;

namespace Reliability.Core.Models
{
    public struct SystemState : ICloneable, IEquatable<SystemState>
    {
        private Predicate<ModuleState[]> _isFunctional { get; }

        public readonly ModuleState[] Modules { get; }
        public bool IsFunctional { get => _isFunctional(Modules); }

        public SystemState(
            ModuleState[] modules,
            Predicate<ModuleState[]> isFunctional)
        {
            Modules = modules;
            _isFunctional = isFunctional;
        }

        public object Clone()
        {
            return new SystemState((ModuleState[])Modules.Clone(), _isFunctional);
        }

        public bool Equals(SystemState other)
        {
            if (!Enumerable.SequenceEqual(Modules, other.Modules))
                return false;

            if (!EqualityComparer<Predicate<ModuleState[]>>.Default.Equals(_isFunctional, other._isFunctional))
                return false;

            return true;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (ModuleState moduleState in Modules)
                stringBuilder.Append(moduleState.ToString());
            return stringBuilder.ToString();
        }
    }
}
