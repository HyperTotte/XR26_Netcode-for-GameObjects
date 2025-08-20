using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NameUI : NetworkBehaviour
{
    [SerializeField] private InputField nameInput;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Text errorText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public static NameUI Instance {  get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        errorText.gameObject.SetActive(false);
        confirmButton.onClick.AddListener(OnConfirmClicked);
    }

    private void OnConfirmClick()
    {
        string name = nameInput.text.Trim();
        if (string.IsNullOrEmpty(name))
        {
            ShowError("Please enter a name.");
            errorText.gameObject.SetActive(true);
            return;
        }

        RequestNameServerRpc(name); // sends a request to see if name avabile. 
    }

    // Update is called once per frame
    public void ShowError(string message)
    {
        errorText.text = message;
        errorText.gameObject.SetActive(True);
    }

    public void HideError()
    {
        errorText.gameObject?.SetActive(false);
    }

    [ServerRpc(RequireOwnership = false)]
    private void RequestNameServerRpc(string name, ServerRpcParams rpcParams = default)
    {
        ulong senderId = rpcParams.Receive.SenderClientId;

        foreach (var kvp in GameSession.PlayerNames)
        {
            if (kvp.Value == name)
            {
                NameRejectedClientRpc(senderId, "Name already taken.");
                return;
            }
                
        }

        GameSession.PlayerNames[senderId] = name; // save name
        NameAcceptedClientRpc(senderId);

    }

    [ClientRpc]
    private void NameRejectedClientRpc(ulong targetId, string reason)
    {
        if (NetworkManager.singleton.LocalclientId == targetId)
        {
            errorText.text = reason;
            errorText.gameObject.SetActive(true);
        }
    }

    [ClientRpc]
    private void NameAcceptedClientRpc(ulong targetId)
    {
        if (NetworkManager.Singleton.LocalClientId == targetId)
        {
            panel.SetActive(false);
        }
    }
}
