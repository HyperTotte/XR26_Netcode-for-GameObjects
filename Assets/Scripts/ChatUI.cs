using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button sendButton;
    [SerializeField] private Transform content;
    [SerializeField] private TextMeshProUGUI messagePrefab;
    [SerializeField] private ScrollRect scrollRect; 

    private void Awake()
    {
        sendButton.onClick.AddListener(OnSendClicked);

        if (ChatManager.Instance != null)
            ChatManager.Instance.OnMessageReceived += OnMessageReceived;
    }

    private void OnDestroy()
    {
        if (ChatManager.Instance != null)
            ChatManager.Instance.OnMessageReceived -= OnMessageReceived;
    }

    private void OnSendClicked()
    {
        if (!string.IsNullOrWhiteSpace(inputField.text))
        {
            ChatManager.Instance.SendMessageToServer(inputField.text);
            inputField.text = "";
        }
    }

    private void OnMessageReceived(ChatMessage message)
    {
        var msgUI = Instantiate(messagePrefab, content);
        msgUI.text = $"[{message.Sender}]: {message.Message}";

        
        Canvas.ForceUpdateCanvases(); // Auto-scroll to bottom
        scrollRect.verticalNormalizedPosition = 0f;
    }
}