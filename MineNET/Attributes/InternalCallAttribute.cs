using System;

namespace MineNET.Attributes
{
    /// <summary>
    /// �����I�Ɏg�p����邱�Ƃ����������ł��B
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