using UnityEngine;
using UnityEngine.UI;

public class NameUI : MonoBehaviour
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
            return;
        }
        var localPlayer = FindObjectOfType<Player>();
        if (localPlayer != null)
        {
            localPlayer.TrySetName(name);
        }
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
}
