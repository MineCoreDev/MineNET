using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MineCraftPENetwork.Packets.RakNet
{
    public abstract class RakNetPacket : Packet
    {
        public byte[] ReadMagic(BinaryReader reader)
        {
            return reader.ReadBytes(16);
        }

        public void WriteMagic(BinaryWriter writer, byte[] magicBuffer)
        {
            if (magicBuffer.Length == 16)
            {
                writer.Write(magicBuffer);
            }
            else
            {
                throw new InvalidDataException("Magic Buffer Invalid");
            }
        }
    }
}
