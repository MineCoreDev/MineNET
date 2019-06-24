using System.Net;
using MineNET.Entities;

namespace MineNET.Network.Interfaces
{
    public interface IPlayerList
    {
        bool AddPlayer(IPlayer player);
        bool RemovePlayer(IPEndPoint endPoint);
        IPlayer GetPlayer(IPEndPoint endPoint);

        IPlayer[] GetPlayers();
    }
}