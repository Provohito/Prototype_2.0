using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DelayWaitingRoomSrc : MonoBehaviourPunCallbacks
{
    private PhotonView myphotonViev;
    /*
    [SerializeField]
    private int multiplayerSceneIndex;
    [SerializeField]
    private int menuSceneIndex;
    [SerializeField]
    private Text timerToStartDisplay;
    private bool startingGame;
    private float timerToStartGame;
    private float notFullgameTimer;
    private float fullGameTimer;
    [SerializeField]
    private float maxWaitTime;
    [SerializeField]
    private float maxFullGameWaitTime;
    */
    private void Start()
    {
        
        myphotonViev = GetComponent<PhotonView>();
        //fullGameTimer = maxFullGameWaitTime;
        // notFullgameTimer = maxWaitTime;
        // timerToStartGame = maxWaitTime;
        if (PhotonNetwork.InRoom)
        {
            Debug.Log("In Room ");
        }
        PlayerCountUpdate();
        CreatePlayer();
        
    }

    bool readyState;
    [SerializeField]
    GameObject readyBtn;
    [SerializeField]
    GameObject cancelBtn;


    private int playerCount;
    private int roomSize;

    [SerializeField]
    private Text playerCountDisplay;
    [SerializeField]
    private int minPlayerToStart;

    [SerializeField]
    GameObject playerDisplayPanel;

    GameObject player;

    private bool readyToStart;

    private bool readyToCountDown;

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
        playerCountDisplay.text = playerCount + ":" + roomSize;
        Debug.Log(GetPlayerCount());
        if (playerCount == roomSize)
        {
            readyToStart = true;
        }
        else if (playerCount >= minPlayerToStart)
        {
            readyToCountDown = true;
        }
        else
        {
            readyToCountDown = false;
            readyToStart = false;
        }
    }

    void Update()
    {
        FindPlayer();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerCountUpdate();

        myphotonViev.RPC("RPC_SetPlayer", RpcTarget.All);       
    }

    void CreatePlayer()
    {
        PhotonNetwork.Instantiate("item", playerDisplayPanel.transform.position, Quaternion.identity).transform.SetParent(playerDisplayPanel.transform);
        Debug.Log("1");
    }

   [PunRPC]
   void RPC_SetPlayer()
    {
        FindPlayer();
        Debug.Log("2");
    }

    void FindPlayer()
    {
        Debug.Log("3");

        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(allPlayers.Length);
        foreach (var item in allPlayers)
        {
            item.transform.SetParent(playerDisplayPanel.transform);
        }
    }
    //[PunRPC]
    /*
    private void RPC_SendTimer(float timeIn)
    {
        timerToStartGame = timeIn;
        notFullgameTimer = timeIn;
        if (timeIn < fullGameTimer)
        {
            fullGameTimer = timeIn;
        }
    }
    
    /*
   public override void OnPlayerLeftRoom(Player otherPlayer)
   {
       PlayerCountUpdate();
   }

   private void Update()
   {
       WaitingForMorePlayers();
   }

   private void WaitingForMorePlayers()
   {
       if (playerCount <= 1)
       {
           ResetTimer();
       }

       if (readyToStart)
       {
           fullGameTimer -= Time.deltaTime;
           timerToStartGame = fullGameTimer;
       }
       else if(readyToCountDown)
       {
           notFullgameTimer -= Time.deltaTime;
           timerToStartGame = notFullgameTimer;
       }

       string tempTimer = string.Format("{0:00}", timerToStartGame);
       timerToStartDisplay.text = tempTimer;

       if (timerToStartGame <= 0f)
       {
           if (startingGame)
           {
               return;
           }
           StartGame();
       }
   }

   private void ResetTimer()
   {
       timerToStartGame = maxWaitTime;
       notFullgameTimer = maxWaitTime;
       fullGameTimer = maxFullGameWaitTime;
   }

   public void StartGame()
   {
       startingGame = true;
       if (!PhotonNetwork.IsMasterClient)
           return;
       PhotonNetwork.CurrentRoom.IsOpen = false;
       PhotonNetwork.LoadLevel(multiplayerSceneIndex);
   }

   public void DelayCancel()
   {
       PhotonNetwork.LeaveRoom();
       SceneManager.LoadScene(menuSceneIndex);
   }
   */

}
