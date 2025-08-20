using UnityEngine;
using Unity.Netcode;
using System;

public class ChatManager : NetworkBehaviour //relay the messages. receiver of meesages and relay to other clients
{
    public static ChatManager Instance { get; private set; } // start of singelton. clients currently only need one

    public event Action<ChatMessage> OnMessageReceived; // Event so each client updates chat when another client has sent a message.

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SendMessageToServer(string message) //For UI to call, which then call the ServerRpc = UI dosen't need to know Rpc.
    {
        if (string.IsNullOrWhiteSpace(message)) return;

        SendMessageServerRpc(message); //message = text user typed in
    }

    [ServerRpc(RequireOwnership = false)]
    private void SendMessageServerRpc(string message, ServerRpcParams rpcParams = default)
    {
        ulong senderId = rpcParams.Receive.SenderClientId;

        string senderName = "Unknown";
        if (NetworkManager.Singleton.ConnectedClients.TryGetValue(senderId, out var client))
        {
            var playerObj = client.PlayerObject;
            if (playerObj != null)
            {
                var player = playerObj.GetComponent<Player>();
                if (player != null)
                    senderName = player.PlayerName.Value.ToString();
            }
        }

        ChatMessage chatMessage = new ChatMessage(senderName, message);
        ReceiveMessageClientRpc(chatMessage);
    }

    [ClientRpc]
    private void ReceiveMessageClientRpc(ChatMessage message)
    {
        OnMessageReceived?.Invoke(message);
    }
}