using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCraftPENetwork
{
    abstract class RakNet
    {
        public const string VERSION = "0.8.1";
        public const int PROTOCOL = 6;
        public static readonly ReadOnlyCollection<byte> MAGIC = Array.AsReadOnly(new byte[16]
        {
            0x00,
            0xff,
            0xff,
            0x00,
            0xfe,
            0xfe,
            0xfe,
            0xfe,
            0xfd,
            0xfd,
            0xfd,
            0xfd,
            0x12,
            0x34,
            0x56,
            0x78
        });

        public const int PRIORITY_NORMAL = 0;
        public const int PRIORITY_IMMEDIATE = 1;

        public const byte FLAG_NEED_ACK = 8;

        /*
	     * Internal Packet:
	     * int32 (length without this field)
	     * byte (packet ID)
	     * payload
	     */

        /*
         * ENCAPSULATED payload:
         * byte (identifier length)
         * byte[] (identifier)
         * byte (flags, last 3 bits, priority)
         * payload (binary internal EncapsulatedPacket)
         */

        public const byte PACKET_ENCAPSULATED = 0x01;

        /*
         * OPEN_SESSION payload:
         * byte (identifier length)
         * byte[] (identifier)
         * byte (address length)
         * byte[] (address)
         * short (port)
         * long (clientID)
         */
        public const byte PACKET_OPEN_SESSION = 0x02;

        /*
         * CLOSE_SESSION payload:
         * byte (identifier length)
         * byte[] (identifier)
         * string (reason)
         */
        public const byte PACKET_CLOSE_SESSION = 0x03;

        /*
         * INVALID_SESSION payload:
         * byte (identifier length)
         * byte[] (identifier)
         */
        public const byte PACKET_INVALID_SESSION = 0x04;

        /* TODO: implement this
         * SEND_QUEUE payload:
         * byte (identifier length)
         * byte[] (identifier)
         */
        public const byte PACKET_SEND_QUEUE = 0x05;

        /*
         * ACK_NOTIFICATION payload:
         * byte (identifier length)
         * byte[] (identifier)
         * int (identifierACK)
         */
        public const byte PACKET_ACK_NOTIFICATION = 0x06;

        /*
         * SET_OPTION payload:
         * byte (option name length)
         * byte[] (option name)
         * byte[] (option value)
         */
        public const byte PACKET_SET_OPTION = 0x07;

        /*
         * RAW payload:
         * byte (address length)
         * byte[] (address from/to)
         * short (port)
         * byte[] (payload)
         */
        public const byte PACKET_RAW = 0x08;

        /*
         * BLOCK_ADDRESS payload:
         * byte (address length)
         * byte[] (address)
         * int (timeout)
         */
        public const byte PACKET_BLOCK_ADDRESS = 0x09;

        /*
         * UNBLOCK_ADDRESS payload:
         * byte (address length)
         * byte[] (address)
         */
        public const byte PACKET_UNBLOCK_ADDRESS = 0x10;

        /*
         * No payload
         *
         * Sends the disconnect message, removes sessions correctly, closes sockets.
         */
        public const byte PACKET_SHUTDOWN = 0x7e;

        /*
         * No payload
         *
         * Leaves everything as-is and halts, other Threads can be in a post-crash condition.
         */
        public const byte PACKET_EMERGENCY_SHUTDOWN = 0x7f;

        public static byte[] GetBuffer(byte[] buffer, int start, int len)
        {
            var rBuffer = new List<byte>();
            for(int i = start; i < (start + len); i++)
            {
                if (i > buffer.Length - 1)
                {
                    break;
                }
                rBuffer.Add(buffer[i]);
            }

            return rBuffer.ToArray();
        }

        public static byte[] GetBuffer(byte[] buffer, int start)
        {
            var rBuffer = new List<byte>();
            for (int i = start; i < (buffer.Length); i++)
            {
                if (i > buffer.Length - 1)
                {
                    break;
                }
                rBuffer.Add(buffer[i]);
            }

            return rBuffer.ToArray();
        }
    }
}
