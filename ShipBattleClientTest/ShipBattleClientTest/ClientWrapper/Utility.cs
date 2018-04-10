using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RakNetClientWrapper
{
    public class Utility
    {
        public static byte[] StringToBytes(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }
        public static string BytesToString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
