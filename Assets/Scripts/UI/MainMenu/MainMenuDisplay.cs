using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; //for Text inputfield.

public class MainMenuDisplay : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string gameplaySceneName = "Gameplay";
    [Header("UI")]
    [SerializeField] private TMP_InputField nameInput;

    private void Awake()
    {
        if (nameInput != null)
        {
            nameInput.text = GameSession.PlayerName;
        }
    }

    private void SaveName()
    {
        if (nameInput != null && !string.IsNullOrWhiteSpace(nameInput.text))
        {
            GameSession.PlayerName = nameInput.text.Trim();
        }
    }

    public void StartHost()
    {
        SaveName();
        NetworkManager.Singleton.StartHost();
        NetworkManager.Singleton.SceneManager.LoadScene(gameplaySceneName, LoadSceneMode.Single);
    }

    public void StartServer()
    {
        SaveName();
        NetworkManager.Singleton.StartServer();
        NetworkManager.Singleton.SceneManager.LoadScene(gameplaySceneName, LoadSceneMode.Single);
    }

    public void StartClient()
    {
        SaveName();
        NetworkManager.Singleton.StartClient();
    }
}
