using System.Threading.Tasks;
using MineNET.Events.PlayerEvents;
using MineNET.Network.Packets;
using MineNET.Network.Packets.Data;
using MineNET.Utils;
using MineNET.Values;
using MineNET.Worlds;
using MineNET.Worlds.Data;

namespace MineNET.Entities.Players
{
    public partial class Player
    {
        internal void PacketHandle(DataPacket pk)
        {
            if (pk is LoginPacket)
            {
                this.LoginPacketHandle((LoginPacket) pk);
            }
            else if (pk is ResourcePackClientResponsePacket)
            {
                this.ResourcePackClientResponsePacketHandle((ResourcePackClientResponsePacket) pk);
            }
            else if (pk is RequestChunkRadiusPacket)
            {
                this.RequestChunkRadiusPacketHandle((RequestChunkRadiusPacket) pk);
            }
            else if (pk is MovePlayerPacket)
            {
                this.MovePlayerPacketHandle((MovePlayerPacket) pk);
            }
            else if (pk is TextPacket)
            {
                this.TextPacketHandle((TextPacket) pk);
            }
            else if (pk is CommandRequestPacket)
            {
                this.CommandRequestPacketHandle((CommandRequestPacket) pk);
            }
        }

        private void LoginPacketHandle(LoginPacket pk)
        {
            if (this.IsPreLogined)
                return;

            if (pk.Protocol < ProtocolInfo.CLIENT_PROTOCOL)
            {
                this.SendPlayStatus(PlayStatusPacket.LOGIN_FAILED_CLIENT);
                this.Close("disconnectionScreen.outdatedClient");
                return;
            }
            else if (pk.Protocol > ProtocolInfo.CLIENT_PROTOCOL)
            {
                this.SendPlayStatus(PlayStatusPacket.LOGIN_FAILED_SERVER);
                this.Close("disconnectionScreen.outdatedServer");
                return;
            }

            PlayerPreLoginEventArgs playerPreLoginEvent = new PlayerPreLoginEventArgs(this, "");
            PlayerEvents.OnPlayerPreLogin(playerPreLoginEvent);
            if (playerPreLoginEvent.IsCancel)
            {
                this.Close(playerPreLoginEvent.KickMessage);
                return;
            }

            this.IsPreLogined = true;

            this.LoginData = pk.LoginData;
            this.Name = pk.LoginData.DisplayName;
            this.DisplayName = this.Name;

            this.ClientData = pk.ClientData;

            Player[] players = Server.Instance.GetPlayers();
            for (int i = 0; i < players.Length; ++i)
            {
                if (players[i] != this)
                {
                    //TODO
                }
            }

            this.SendPlayStatus(PlayStatusPacket.LOGIN_SUCCESS);

            ResourcePacksInfoPacket resourcePacksInfoPacket = new ResourcePacksInfoPacket();
            this.SendPacket(resourcePacksInfoPacket);
        }

        private void ResourcePackClientResponsePacketHandle(ResourcePackClientResponsePacket pk)
        {
            if (this.PackSyncCompleted)
            {
                return;
            }
            else if (pk.ResponseStatus == ResourcePackClientResponsePacket.STATUS_REFUSED)
            {
                this.Close("disconnectionScreen.resourcePackn");
            }
            else if (pk.ResponseStatus == ResourcePackClientResponsePacket.STATUS_SEND_PACKS)
            {
                //TODO: ResourcePackDataInfoPacket
            }
            else if (pk.ResponseStatus == ResourcePackClientResponsePacket.STATUS_HAVE_ALL_PACKS)
            {
                ResourcePackStackPacket resourcePackStackPacket = new ResourcePackStackPacket();
                this.SendPacket(resourcePackStackPacket);

                this.HaveAllPacks = true;
            }
            else if (pk.ResponseStatus == ResourcePackClientResponsePacket.STATUS_COMPLETED && this.HaveAllPacks)
            {
                this.PackSyncCompleted = true;

                this.ProcessLogin();
            }
        }

        private void RequestChunkRadiusPacketHandle(RequestChunkRadiusPacket pk)
        {
            int chunkSize = this.FixRadius(pk.Radius);
            ChunkRadiusUpdatedPacket chunkRadiusUpdatedPacket = new ChunkRadiusUpdatedPacket();
            chunkRadiusUpdatedPacket.Radius = chunkSize;
            this.RequestChunkRadius = chunkSize;
            //Logger.Info("%server_chunkRadius", pk.Radius, chunkRadiusUpdatedPacket.Radius);
            SendPacket(chunkRadiusUpdatedPacket);
        }

        private void MovePlayerPacketHandle(MovePlayerPacket pk)
        {
            //TODO: MoveCheck...
            Vector3 pos = pk.Pos;
            Vector3 direction = pk.Direction;
            this.X = pos.X;
            this.Y = pos.Y;
            this.Z = pos.Z;
            this.Pitch = direction.X;
            this.Yaw = direction.Y;
            //this.SendPosition(pos, direction, MovePlayerPacket.MODE_RESET);
        }

        private void TextPacketHandle(TextPacket pk)
        {
            if (pk.Type == TextPacket.TYPE_CHAT)
            {
                pk.Message = pk.Message.Trim();
                if (pk.Message != "" && pk.Message.Length < 256)
                {
                    pk.Message = $"<{this.Name}§f> {pk.Message}§f";
                    Logger.Info(pk.Message);
                    Player[] players = Server.Instance.GetPlayers();
                    for (int i = 0; i < players.Length; ++i)
                    {
                        players[i].SendPacket(pk);
                    }
                }
            }
        }

        private void CommandRequestPacketHandle(CommandRequestPacket pk)
        {
            string command = pk.Command.Remove(0, 1);
            //TODO : event
            Server.Instance.CommandManager.HandlePlayerCommand(this, command);
        }

        private void ProcessLogin()
        {
            if (this.IsLogined)
            {
                return;
            }
            PlayerLoginEventArgs playerLoginEvent = new PlayerLoginEventArgs(this, "");
            PlayerEvents.OnPlayerLogin(playerLoginEvent);
            if (playerLoginEvent.IsCancel)
            {
                this.Close(playerLoginEvent.KickMessage);
                return;
            }

            this.IsLogined = true;

            this.LoadData();

            this.X = 128;
            this.Y = 6;
            this.Z = 128;

            StartGamePacket startGamePacket = new StartGamePacket();
            startGamePacket.EntityUniqueId = this.EntityID;
            startGamePacket.EntityRuntimeId = this.EntityID;
            startGamePacket.PlayerGamemode = 1;
            startGamePacket.PlayerPosition = new Vector3(this.X, this.Y, this.Z);
            startGamePacket.Direction = new Vector2(this.Yaw, this.Pitch);
            startGamePacket.WorldGamemode = 0;
            startGamePacket.Difficulty = 1;
            startGamePacket.SpawnX = 128;
            startGamePacket.SpawnY = 6;
            startGamePacket.SpawnZ = 128;
            startGamePacket.WorldName = "world";
            this.SendPacket(startGamePacket);

            this.SendPlayerAttribute();

            AvailableCommandsPacket availableCommandsPacket = new AvailableCommandsPacket();
            availableCommandsPacket.commands = Server.Instance.CommandManager.CommandList;
            this.SendPacket(availableCommandsPacket);

            AdventureSettingsPacket adventureSettingsPacket = new AdventureSettingsPacket();
            adventureSettingsPacket.SetFlag(AdventureSettingsPacket.WORLD_IMMUTABLE, false);
            adventureSettingsPacket.SetFlag(AdventureSettingsPacket.NO_PVP, false);
            adventureSettingsPacket.SetFlag(AdventureSettingsPacket.AUTO_JUMP, false);
            adventureSettingsPacket.SetFlag(AdventureSettingsPacket.ALLOW_FLIGHT, false);
            adventureSettingsPacket.SetFlag(AdventureSettingsPacket.NO_CLIP, false);
            adventureSettingsPacket.SetFlag(AdventureSettingsPacket.FLYING, false);
            adventureSettingsPacket.EntityUniqueId = this.EntityID;
            this.SendPacket(adventureSettingsPacket);

            this.SendDataProperties();

            //InventoryContent
            //MobArmorEquipment
            //inventoryContent
            //MobEquipment
            //InventorySlot

            this.SendFastChunk();
        }

        private async void SendFastChunk()
        {
            await Task.Run(() =>
            {
                for (int i = ((int) this.X >> 4) - this.RequestChunkRadius; i < ((int) this.X >> 4) + this.RequestChunkRadius; ++i)
                {
                    for (int j = ((int) this.Z >> 4) - this.RequestChunkRadius; j < ((int) this.Z >> 4) + this.RequestChunkRadius; ++j)
                    {
                        new Chunk(i, j).TestChunkSend(this);
                    }
                }
                PlayerJoinEventArgs playerJoinEvent = new PlayerJoinEventArgs(this, "", "");
                PlayerEvents.OnPlayerJoin(playerJoinEvent);
                if (playerJoinEvent.IsCancel)
                {
                    this.Close(playerJoinEvent.KickMessage);
                    return;
                }

                this.SendPlayStatus(PlayStatusPacket.PLAYER_SPAWN);
            });

            this.HasSpawned = true;

            GameRules rules = new GameRules();
            rules.Add(new GameRule<bool>("ShowCoordinates", true));

            GameRulesChangedPacket gameRulesChangedPacket = new GameRulesChangedPacket();
            gameRulesChangedPacket.GameRules = rules;
            this.SendPacket(gameRulesChangedPacket);

            PlayerListEntry entry = new PlayerListEntry(this.LoginData.ClientUUID, this.EntityID, this.Name, this.ClientData.DeviceOS, this.ClientData.Skin, this.LoginData.XUID);
            Server.Instance.AddPlayer(this, entry);
        }
    }
}
