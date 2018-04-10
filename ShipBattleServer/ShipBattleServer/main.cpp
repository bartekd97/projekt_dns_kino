#include <iostream>
#include <stdlib.h>
#include "include.h"

using namespace std;
using namespace RakNet;

RakPeerInterface* g_ServerPeer;

int main()
{
	cout << "Initializing..." << endl;
	SocketDescriptor serverSD(SERVER_PORT,0);
	g_ServerPeer = RakPeerInterface::GetInstance();
	cout << "Starting server..." << endl;
	g_ServerPeer->Startup(MAX_CLIENTS, &serverSD, 1);
	g_ServerPeer->SetMaximumIncomingConnections(MAX_CLIENTS);
	
	cout << "Awaiting for incoming packets..." << endl;
	Packet *packet;
	while (true)
	{
		for (packet = g_ServerPeer->Receive(); packet; g_ServerPeer->DeallocatePacket(packet), packet = g_ServerPeer->Receive())
		{
			switch (packet->data[0])
			{
			case ID_NEW_INCOMING_CONNECTION:
				cout << "New client connected" << endl;
				break;
			case ID_CONNECTION_LOST:
				cout << "Client lost connection" << endl;
				break;
			case ID_DISCONNECTION_NOTIFICATION:
				cout << "Client has disconnected" << endl;
				break;

			case ID_LOGIN_ATTEMPT:
			{
				BitStream bs(packet->data, packet->length, false);
				bs.IgnoreBytes(sizeof(MessageID));
				RakString login;
				bs.Read(login);
				cout << "Login attempt: " << login.C_String() << endl;

				BitStream bsOut;
				bsOut.Write((MessageID)ID_LOGIN_REESULT);
				bsOut.Write((char)LoginResult::SUCCESS);
				g_ServerPeer->Send(&bsOut, LOW_PRIORITY, RELIABLE, CHANNEL_REL_SERVER_MESSAGES, packet->guid, false);
			}
				break;

			default:
				break;
			}
		}
	}

	cout << "Destroying instance..." << endl;
	RakPeerInterface::DestroyInstance(g_ServerPeer);
	return 0;
}