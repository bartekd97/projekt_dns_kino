#include "Utility.h"

void CreateMessagePacket(BitStream &bs, RakString &str, GameMessages msg)
{
	bs.Write((MessageID)msg);
	bs.Write(str);
}
void CreateMessagePacket(BitStream &bs, string &str, GameMessages msg)
{
	RakString rstr = RakString(str.c_str());
	CreateMessagePacket(bs, rstr, msg);
}
void CreateMessagePacket(BitStream &bs, char *str, GameMessages msg)
{
	RakString rstr = RakString(str);
	CreateMessagePacket(bs, rstr, msg);
}

RakString ReadMessagePacket(Packet *packet)
{
	BitStream bs(packet->data, packet->length, false);
	bs.IgnoreBytes(sizeof(MessageID));
	RakString rstr;
	bs.Read(rstr);
	return rstr;
}
void ReadMessagePacket(Packet *packet, RakString &str)
{
	BitStream bs(packet->data, packet->length, false);
	bs.IgnoreBytes(sizeof(MessageID));
	bs.Read(str);
}