#pragma once
#include <string.h>

#include "..\..\ShipBattleServer\ShipBattleServer\RakNet\RakPeerInterface.h"
#include "..\..\ShipBattleServer\ShipBattleServer\RakNet\MessageIdentifiers.h"
#include "..\..\ShipBattleServer\ShipBattleServer\RakNet\BitStream.h"
#include "..\..\ShipBattleServer\ShipBattleServer\RakNet\RakNetTypes.h"
#include "..\..\ShipBattleServer\ShipBattleServer\RakNet\RakSleep.h"

#include "..\..\ShipBattleServer\ShipBattleServer\Shared\include.h"

extern bool IsInitialized = false;
extern RakNet::RakPeerInterface* g_MyPeer;
extern RakNet::RakNetGUID g_GUID;