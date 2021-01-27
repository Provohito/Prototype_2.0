using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonLauncher : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private GameObject startWindow;
    //[SerializeField]
    //private GameObject hostWindow;
    //[SerializeField]
    //private InputField nameRoom;

    [SerializeField]
    private Text Logtext;
    [SerializeField]
    private InputField namePlayer;
    private void Start()
    {
        
        //Log("Player name set to " + PhotonNetwork.NickName);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion ="1";
        PhotonNetwork.ConnectUsingSettings();
        
    }

    public override void OnConnectedToMaster()
    {
        Log("On connect to master");
    }

    public void CreateRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.NickName = namePlayer.text;
            //PlayerPrefs.SetString("Player1",namePlayer.text);
            PhotonNetwork.CreateRoom("room1");
            
        }
        
    }
    public void ConnectToRoom()
    {
        PhotonNetwork.NickName = namePlayer.text;
        PhotonNetwork.JoinRoom("room1");
        
    }

    public override void OnJoinedRoom()
    {
        Log("SomeOne join room");
        PhotonNetwork.LoadLevel("Room");
    }

    private void Log(string value)
    {
        Debug.Log(value);
        Logtext.text += "\n";
        Logtext.text += value;

    }

}
