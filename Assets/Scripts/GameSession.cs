using UnityEngine;

public class GameSession : MonoBehaviour
{
   public static GameSession Instance { get; private set; }

    public string LocalPlayerName { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetLocalPlayerName(string name)
    {
        LocalPlayerName = name;
    }
}
