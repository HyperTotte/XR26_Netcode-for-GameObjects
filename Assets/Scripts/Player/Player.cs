using UnityEngine;
using Unity.Netcode;
using Unity.Collections;

public class Player : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public NetworkVariable<FixedString32Bytes> PlayerName = new NetworkVariable<FixedString32Bytes>();

    private void Update()
    {
        if (!IsOwner) return;

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Vector3 move = input * moveSpeed * Time.deltaTime;
        MoveServerRpc(move);
    }

    [ServerRpc]
    private void MoveServerRpc(Vector3 move, ServerRpcParams rpcParams = default)
    {
        transform.position += move;
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            string name = GameSession.Instance != null ? GameSession.Instance.LocalPlayerName : "Player";
            SetNameServerRpc(name);
        }
    }

    [ServerRpc]
    private void SetNameServerRpc(string name, ServerRpcParams rpcParams = default)
    {
        PlayerName.Value = new FixedString32Bytes(name);
    }
}