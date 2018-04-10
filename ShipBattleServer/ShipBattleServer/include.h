#pragma once
#include <string.h>

#include "RakNet\RakPeerInterface.h"
#include "RakNet\MessageIdentifiers.h"
#include "RakNet\BitStream.h"
#include "RakNet\RakNetTypes.h"
#include "RakNet\RakSleep.h"

#include "Shared\include.h"

#define MAX_CLIENTS 128
#define SERVER_PORT 38383

extern RakNet::RakPeerInterface* g_ServerPeer;