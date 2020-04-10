namespace Wd3eCore.Environment.Shell.Descriptor.Models
{
    /// <summary>
    /// shell参数是可以分配给shell中特定组件的自定义值。
    /// </summary>
    public class ShellParameter
    {
        public string Component { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
