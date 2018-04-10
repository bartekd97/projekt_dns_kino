#include "include.h"
#include "dllexport.h"

using namespace RakNet;

Packet *packet;
RakPeerInterface* g_MyPeer;
RakNetGUID g_GUID;

void InitAndConnect(char* ip, int port)
{
	if (IsInitialized)
		return;

	SocketDescriptor sd;
	g_MyPeer = RakPeerInterface::GetInstance();
	g_MyPeer->Startup(1, &sd, 1);
	g_MyPeer->Connect(ip, port, 0, 0);

	IsInitialized = true;
}
void SaveGUID()
{
	if (packet)
	{
		g_GUID = packet->guid;
	}
}
bool GetPacketType(unsigned char &ptype)
{
	if (!IsInitialized)
		return false;

	packet = g_MyPeer->Receive();
	if (packet)
	{
		ptype = packet->data[0];
		return true;
	}
	else
		return false;
}
void DeallocatePacket()
{
	if (packet)
	{
		g_MyPeer->DeallocatePacket(packet);
		packet = NULL;
	}
}

int GetPacketRaw(char *buff)
{
	if (packet)
	{
		int len = packet->length;
		for (int i = 0; i < len; i++)
			buff[i] = packet->data[i];
		return len;
	}
	else
		return 0;
}


void SendLogin(char* login)
{
	BitStream bs;
	bs.Write((MessageID)ID_LOGIN_ATTEMPT);
	bs.Write(RakString(login));
	g_MyPeer->Send(&bs, LOW_PRIORITY, RELIABLE, CHANNEL_REL_SERVER_MESSAGES, g_GUID, false);
}