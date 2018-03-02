namespace MineNET.Events
{
    public interface ICancellable
    {
        bool IsCancel { get; set; }
    }
}
