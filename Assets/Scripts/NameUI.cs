using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Data.SqlTypes;

public class NameUI : MonoBehaviour // for mainmenu scene. handles the nameinput field.
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Button confirmButton;
    [SerializeField] private GameObject panel;

    private void Awake()
    {
        
        confirmButton.onClick.AddListener(OnConfirmClicked); //ready for when confirmbutton is pressed.
    }

    private void OnConfirmClicked()
    {
        
        string inputName = nameInput.text.Trim(); // read input. so we can store name and use for chat in gameplay scene.

        
        if (string.IsNullOrEmpty(inputName)) // had problem where gameplay scene didn't start because name was null and coulden't start. 
        {
            inputName = "Player" + Random.Range(1000, 9999); //this is to make sure that client has name and dosen't disrupt scene change.
        }

       
        GameSession.Instance.SetLocalPlayerName(inputName); // set name in Gamesession.
        panel.SetActive(false); // so player can't change name and show that it works...

        Debug.Log("Player name set to: " + inputName);
    }
}