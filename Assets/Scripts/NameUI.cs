using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NameUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Button confirmButton;
    [SerializeField] private GameObject panel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        confirmButton.onClick.AddListener(OnConfirmClicked);
    }

    // Update is called once per frame
    private void OnConfirmClicked()
    {
        string inputName = nameInput.text.Trim();
        if (string.IsNullOrEmpty(inputName))
            return;
        GameSession.Instance.SetLocalPlayerName(inputName);
        panel.SetActive(false);
    }
}
