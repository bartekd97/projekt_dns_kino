#pragma once
#include "..\RakNet\MessageIdentifiers.h"

enum GameMessages
{
	ID_SERVER_MESSAGE = ID_USER_PACKET_ENUM,
	ID_LOGIN_ATTEMPT,
	ID_LOGIN_REESULT
};

enum LoginResult
{
	SUCCESS
};