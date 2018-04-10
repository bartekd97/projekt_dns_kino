using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RakNetClientWrapper;

namespace ShipBattleClientTest
{
    public partial class Form1 : Form
    {
        private byte[] pBuff = new byte[4096];

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MessageType mtype;
            for (byte ptype = 0; ClientWrapper.GetPacketType(ref ptype); ClientWrapper.DeallocatePacket())
            {
                mtype = (MessageType)ptype;

                Log("Message type: " + mtype.ToString());
                ClientWrapper.GetPacketRaw(pBuff);
                switch (mtype)
                {
                    case MessageType.ID_CONNECTION_REQUEST_ACCEPTED:
                        ClientWrapper.SaveGUID();
                        break;
                    case MessageType.ID_LOGIN_REESULT:
                        LoginResult lres = (LoginResult)pBuff[1];
                        Log("Login result: " + lres.ToString());
                        break;
                    default:
                        break;
                }
            }
        }


        private void Log(string str)
        {
            richTextBox1.AppendText(str + Environment.NewLine);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Log("Connecting...");
            int port = Convert.ToInt32(tbPort.Text);
            ClientWrapper.InitAndConnect(tbIP.Text, port);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Log("Logging in...");
            ClientWrapper.SendLogin(tbLogin.Text);
        }
    }
}
