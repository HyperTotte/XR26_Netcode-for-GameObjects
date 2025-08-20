using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; } // start of singelton
    public string LocalPlayerName { get; private set; } //storage for user name. 

    private void Awake()
    {
        if (Instance != null && Instance != this) //ensure only one instance is.
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetLocalPlayerName(string name) //so MainMenuUI can set name. 
    {
        LocalPlayerName = name;
    }
}