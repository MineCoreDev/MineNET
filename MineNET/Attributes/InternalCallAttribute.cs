using System;

namespace MineNET.Attributes
{
    /// <summary>
    /// 内部的に使用されることを示す属性です。
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Class | 
        AttributeTargets.Constructor | 
        AttributeTargets.Delegate | 
        AttributeTargets.Enum | 
        AttributeTargets.Event | 
        AttributeTargets.Field |
        AttributeTargets.Interface |
        AttributeTargets.Method | 
        AttributeTargets.Parameter | 
        AttributeTargets.Property | 
        AttributeTargets.Struct)]
    internal class InternalCallAttribute : Attribute
    {
    }
}