using System;
using System.Net;
using MineNET.Entities;
using MineNET.Network.Interfaces;

namespace MineNET.Network
{
    public class PlayerList : IPlayerList
    {
        public bool AddPlayer(IPlayer player)
        {
            throw new NotImplementedException();
        }

        public bool RemovePlayer(IPEndPoint endPoint)
        {
            throw new NotImplementedException();
        }

        public IPlayer GetPlayer(IPEndPoint endPoint)
        {
            throw new NotImplementedException();
        }

        public IPlayer[] GetPlayers()
        {
            throw new NotImplementedException();
        }
    }
}