#pragma once
#include <iostream>
#include <string.h>

#include "Enum.h"
#include "Struct.h"

#include "..\RakNet\RakPeerInterface.h"
#include "..\RakNet\BitStream.h"
#include "..\RakNet\RakString.h"

using namespace RakNet;
using namespace std;

void CreateMessagePacket(BitStream &bs, RakString &str, GameMessages msg);
void CreateMessagePacket(BitStream &bs, string &str, GameMessages msg);
void CreateMessagePacket(BitStream &bs, char *str, GameMessages msg);
RakString ReadMessagePacket(Packet *packet);
void ReadMessagePacket(Packet *packet, RakString &str);