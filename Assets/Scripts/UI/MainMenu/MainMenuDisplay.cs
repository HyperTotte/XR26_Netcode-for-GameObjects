using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuDisplay : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string gameplaySceneName = "Gameplay";
    [SerializeField] private TMP_InputField nameInput;

    public void StartHost()
    {
        GameSession.Instance.SetLocalPlayerName(nameInput.text);
        NetworkManager.Singleton.StartHost();
        NetworkManager.Singleton.SceneManager.LoadScene(gameplaySceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void StartServer()
    {
        GameSession.Instance.SetLocalPlayerName(nameInput.text);
        NetworkManager.Singleton.StartServer();
        NetworkManager.Singleton.SceneManager.LoadScene(gameplaySceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void StartClient()
    {
        GameSession.Instance.SetLocalPlayerName(nameInput.text);
        NetworkManager.Singleton.StartClient();
        // DO NOT call LoadScene for client
    }
}
