using System.Collections.Generic;
using MineNET.Entities.Metadata;
using MineNET.NBT.Tags;
using MineNET.Network.Packets;
using MineNET.Values;
using MineNET.Worlds;

namespace MineNET.Entities
{
    public abstract class Entity : ILocation
    {
        private static long nextEntityId = 0;

        private EntityMetadataManager dataProperties = new EntityMetadataManager();

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
                return this.displayName;
            }

            set
            {
                this.displayName = value;
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

        public void sendData(params Player[] players)
        {
            for (int i = 0; i < players.Length; ++i)
            {
                SetEntityDataPacket pk = new SetEntityDataPacket();
                pk.EntityRuntimeId = this.EntityID;
                pk.EntityData = this.dataProperties;
                players[i].SendPacket(pk);
            }
        }

        internal virtual void OnUpdate()
        {

        }

        public virtual void Close()
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
