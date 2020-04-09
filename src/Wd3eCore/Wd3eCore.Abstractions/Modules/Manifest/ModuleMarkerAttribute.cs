using System;

namespace Wd3eCore.Modules.Manifest
{
    /// <summary>
    /// 将一个程序集标记为指定类型的模块，在构建时自动生成。
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public class ModuleMarkerAttribute : ModuleAttribute
    {
        private string _type;

        public ModuleMarkerAttribute(string name, string type)
        {
            Name = name;
            _type = type;
        }

        public override string Type => _type;
    }
}
