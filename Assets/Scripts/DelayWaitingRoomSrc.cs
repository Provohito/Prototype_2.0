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

    private bool readyToStart = false;

    private bool readyToCountDown;

    int viewId;
    GameObject ounPlayer;
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

    

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerCountUpdate();

        myphotonViev.RPC("RPC_SetPlayer", RpcTarget.AllBuffered);
        myphotonViev.RPC("RPC_FixPlayers", RpcTarget.OthersBuffered, viewId, PhotonNetwork.NickName);
    }
    [PunRPC]
    void RPC_FixPlayers(int ViewId, string namePlayer)
    {
        Debug.Log(ViewId);
        Debug.Log(namePlayer);
        Debug.Log("Fix");
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (var item in allPlayers)
        {
            if (item.GetComponent<PhotonView>().ViewID == ViewId)
            {
                Debug.Log(item.transform.GetChild(0).GetChild(1).name);
                Debug.Log(item.name);
                item.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = namePlayer;
            }  
        }
    }
    void CreatePlayer()
    {
        ounPlayer = PhotonNetwork.Instantiate("item", new Vector3(0, 0, 0), Quaternion.identity, 0);
        ounPlayer.transform.SetParent(playerDisplayPanel.transform);
        viewId = ounPlayer.GetComponent<PhotonView>().ViewID;
        GameObject namePlayer = ounPlayer.transform.GetChild(0).GetChild(1).gameObject;
        namePlayer.GetComponent<Text>().text = PhotonNetwork.NickName;
        //Debug.Log("1");
    }

    

    [PunRPC]
   void RPC_SetPlayer()
    {
        Debug.Log("Set");
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (var item in allPlayers)
        {
             item.transform.SetParent(playerDisplayPanel.transform);
            
        }
    }

    public void Ready(Text textBtn)
    {
        GameObject completedChoice = ounPlayer.transform.GetChild(0).GetChild(4).gameObject;

        if (readyToStart == false)
        {
            textBtn.text = "Готов !";
            completedChoice.GetComponent<Text>().text = "Готов !";
        }
        else
        {
            textBtn.text = "Не готов (";
            completedChoice.GetComponent<Text>().text = "Не готов (";
        }           

        readyToStart = !readyToStart;
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
    */
   public override void OnPlayerLeftRoom(Player otherPlayer)
   {
       PlayerCountUpdate();
   }
    /*
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
