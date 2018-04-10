#pragma once
#define EXPORT __declspec(dllexport)

extern "C" {
	EXPORT void InitAndConnect(char* ip, int port);
	EXPORT void SaveGUID();
	EXPORT bool GetPacketType(unsigned char &ptype);
	EXPORT void DeallocatePacket();

	EXPORT int GetPacketRaw(char *buff);

	EXPORT void SendLogin(char* login);
}