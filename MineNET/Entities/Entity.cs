using System.Collections.Generic;
using MineNET.NBT.Tags;
using MineNET.Network.Packets;
using MineNET.Values;
using MineNET.Worlds;

namespace MineNET.Entities
{
    public abstract class Entity : ILocation
    {
        private static long nextEntityId = 0;

        protected List<Player> viewers = new List<Player>();

        public CompoundTag namedTag;

        public Entity()
        {
            this.EntityID = Entity.nextEntityId++;
        }

        public long EntityID { get; }

        public bool IsPlayer { get; protected set; }

        public abstract string Name { get; protected set; }

        string displayName;
        public string DisplayName
        {
            get
            {
                return displayName;
            }

            set
            {
                displayName = value;
                //TODO: SendEntityData
            }
        }

        public Player[] GetViewers()
        {
            return this.viewers.ToArray();
        }

        public void SendPacketViewers(DataPacket pk)
        {
            Player[] players = this.GetViewers();
            for (int i = 0; i < players.Length; ++i)
            {
                if (players[i].HasSpawned)
                {
                    players[i].SendPacket(pk);
                }
            }
        }

        public virtual void SetMotion(Vector3 motion)
        {
            SetEntityMotionPacket pk = new SetEntityMotionPacket();
            pk.EntityRuntimeId = this.EntityID;
            pk.Motion = motion;

            this.SendPacketViewers(pk);
        }

        internal virtual void OnUpdate()
        {

        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public float Yaw { get; set; }
        public float Pitch { get; set; }

        public World World { get; set; }
    }
}
