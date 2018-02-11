using System;

namespace MineNET.Utils.Config.Yaml.Attributes
{
    //TODO: YamlSpace
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    class YamlSpaceAttribute : Attribute
    {

    }
}
