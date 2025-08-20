using UnityEngine;
using System.Collections.Generic;
using Unity.Netcode;

public static class GameSession
{
    public static Dictionary<ulong, string> PlayerNames = new Dictionary<ulong, string>();
    public static string LocalPlayerName
    {
        get
        {
            if (NetworkManager.Singleton != null && PlayerNames.TryGetValue(NetworkManager.Singleton.LocalClientId, out var name))
            {
                return name;
            }
            return "Unknown";
        }
    }
}
