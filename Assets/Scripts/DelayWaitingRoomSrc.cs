using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class DelayWaitingRoomSrc : MonoBehaviourPunCallbacks
{
    private PhotonView myphotonViev;

    [SerializeField]
    GameObject readyBtn;

    private int playerCount;
    private int roomSize;

    [SerializeField]
    private Text playerCountDisplay;

    private int minPlayerToStart;
    private bool readyToStartCount;

    [SerializeField]
    GameObject playerDisplayPanel;

    private bool readyToStart = false;

    int viewId;
    GameObject ounPlayer;
    private void Start()
    {
        myphotonViev = GetComponent<PhotonView>();
        PlayerCountUpdate();
        CreatePlayer();
    }
    private void Update()
    {
        WaitingForMorePlayers();
    }
    static int GetPlayerCount()
    {
        if (PhotonNetwork.CurrentRoom != null)
        {
            return PhotonNetwork.CurrentRoom.PlayerCount;
        }
        return 0;
    }
    private void PlayerCountUpdate()
    {
        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        playerCount = GetPlayerCount();
        minPlayerToStart = roomSize / 2 + 1;
        playerCountDisplay.text = playerCount + ":" + roomSize;
        if (playerCount == roomSize || playerCount >= minPlayerToStart)
        {
            readyToStartCount = true;
        }
        else
            readyToStartCount = false;
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerCountUpdate();

        myphotonViev.RPC("RPC_SetPlayer", RpcTarget.AllBuffered);
        myphotonViev.RPC("RPC_FixPlayers", RpcTarget.OthersBuffered, viewId, PhotonNetwork.NickName);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PlayerCountUpdate();
    }
    [PunRPC]
    void RPC_FixPlayers(int ViewId, string namePlayer)
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (var item in allPlayers)
        {
            if (item.GetComponent<PhotonView>().ViewID == ViewId & !myphotonViev.IsMine)
                item.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = namePlayer;
        }
    }
    [PunRPC]
    void RPC_SetPlayer()
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (var item in allPlayers)
        {
            item.transform.SetParent(playerDisplayPanel.transform);

        }
    }
    void CreatePlayer()
    {
        ounPlayer = PhotonNetwork.Instantiate("item", new Vector3(0, 0, 0), Quaternion.identity, 0);
        ounPlayer.transform.SetParent(playerDisplayPanel.transform);
        viewId = ounPlayer.GetComponent<PhotonView>().ViewID;
        GameObject namePlayer = ounPlayer.transform.GetChild(0).GetChild(1).gameObject;
        namePlayer.GetComponent<Text>().text = PhotonNetwork.NickName;
    }



    public void Ready(Text textBtn)
    {
        GameObject completedChoice = ounPlayer.transform.GetChild(0).GetChild(4).gameObject;

        if (readyToStart == false)
        {
            textBtn.text = "Готов !";
            completedChoice.GetComponent<Text>().text = "Готов !";
            myphotonViev.RPC("RPC_Ready", RpcTarget.OthersBuffered, true, PhotonNetwork.NickName);
        }
        else
        {
            textBtn.text = "Не готов (";
            completedChoice.GetComponent<Text>().text = "Не готов (";
            myphotonViev.RPC("RPC_Ready", RpcTarget.OthersBuffered, false, PhotonNetwork.NickName);
        }           

        readyToStart = !readyToStart;

    }
    [PunRPC]
    void RPC_Ready(bool ready, string namePlayer)
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (var item in allPlayers)
        {
            if (item.transform.GetChild(0).GetChild(1).GetComponent<Text>().text == namePlayer)
            {

                if (ready)
                {
                    item.transform.GetChild(0).GetChild(4).GetComponent<Text>().text = "Готов !";
                }
                else
                    item.transform.GetChild(0).GetChild(4).GetComponent<Text>().text = "Не готов (";
            }
        }

    }



    private void WaitingForMorePlayers()
   {
        int countReadyPlayer = 0;
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (var item in allPlayers)
        {
            if (item.transform.GetChild(0).GetChild(4).GetComponent<Text>().text  == "Готов !")
            {
                countReadyPlayer += 1;
            }
           
        }
        if (readyToStart)
       {
            if (countReadyPlayer == playerCount && readyToStartCount == true)
            {
                //StartGame();
                Debug.Log("Победа");
            }
             
       }
   }

   public void StartGame()
   {
       if (!PhotonNetwork.IsMasterClient)
           return;
       PhotonNetwork.CurrentRoom.IsOpen = false;
      // PhotonNetwork.LoadLevel(multiplayerSceneIndex); // Load level
   }

   
   

}
