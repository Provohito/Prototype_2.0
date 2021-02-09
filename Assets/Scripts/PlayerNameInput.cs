using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerNameInput : MonoBehaviour
{

    [SerializeField]
    InputField  nameInputField;
    // Start is called before the first frame update
    private const string playerPrefsNameKey = "PlayerName";

    private void Start() => SetUpInputField();

    public void SetUpInputField()
    {
        if (!PlayerPrefs.HasKey(playerPrefsNameKey)) return;

        string defaultName = PlayerPrefs.GetString(playerPrefsNameKey);

        SetPlayerName(defaultName);
    }

    public void SetPlayerName(string name)
    {
        if (nameInputField.text!= "")
        {
            SavePlayerName();
            Debug.Log("win");
        }
        
    }

    private void SavePlayerName()
    {
        string playerName = nameInputField.text;

        PhotonNetwork.NickName = playerName;

        PlayerPrefs.SetString(playerPrefsNameKey, playerName);
    }
    
}
