namespace System
{
    public interface ICloneable<T> : ICloneable
    {
        new T Clone();
    }
}
