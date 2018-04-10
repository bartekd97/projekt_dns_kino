using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace RakNetClientWrapper
{
    public class ClientWrapper
    {
        const string WRAPDLL = "ShipBattleClientNet.dll";

        [DllImport(WRAPDLL, EntryPoint = "InitAndConnect")]
        private static extern void _InitAndConnect(byte[] ip, int port);
        [DllImport(WRAPDLL, EntryPoint = "SaveGUID")]
        public static extern void SaveGUID();
        [DllImport(WRAPDLL, EntryPoint = "GetPacketType")]
        public static extern bool GetPacketType(ref byte ptype);
        [DllImport(WRAPDLL, EntryPoint = "DeallocatePacket")]
        public static extern void DeallocatePacket();

        [DllImport(WRAPDLL, EntryPoint = "GetPacketRaw")]
        public static extern int GetPacketRaw(byte[] buff);


        [DllImport(WRAPDLL, EntryPoint = "SendLogin")]
        private static extern void _SendLogin(byte[] ip);


        public static void InitAndConnect(string ip, int port)
        {
            _InitAndConnect(Utility.StringToBytes(ip), port);
        }

        public static void SendLogin(string login)
        {
            _SendLogin(Utility.StringToBytes(login));
        }
    }
}