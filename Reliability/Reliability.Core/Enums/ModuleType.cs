namespace Reliability.Core.Enums
{
    [Flags]
    public enum ModuleType
    {
        Software,
        Hardware,
        SoftwareHardware = Software | Hardware,
    }
}
