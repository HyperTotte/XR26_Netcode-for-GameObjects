using Unity.Netcode;
using UnityEngine;

public struct ChatMessage : INetworkSerializable //interface from netcode framework.
{
    public string Sender;
    public string Message;

    public ChatMessage(string sender, string message)
    {
        Sender = sender;
        Message = message;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref Sender); // so it can send via server
        serializer.SerializeValue(ref Message);
    }
}