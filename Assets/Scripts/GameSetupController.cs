using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class GameSetupController : MonoBehaviourPun
{
    [SerializeField]
    private GameObject[] points;
    GameObject player;
    [SerializeField]
    private Text[] numberChooseTheme;
    private PhotonView PV;
    int makeChoice;
    bool makedCh;


    [Header("Timer characteristics")]
    [SerializeField]
    private int themesSceneIndex;
    [SerializeField]
    private float maxFullGameWaitTime;
    [SerializeField]
    private Text timerToStartDisplay;
    private float timerToStartGame;
    private float fullGameTimer;
    private bool startingGame;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        //CreatePlayer();
        fullGameTimer = maxFullGameWaitTime;
        timerToStartGame = maxFullGameWaitTime;
    }

    private void Update()
    {
        WaitingForMorePlayers();
    }

    /*private void CreatePlayer()
    {
        
        player = PhotonNetwork.Instantiate("playerPref", Vector3.zero, Quaternion.identity);

        if (player.GetPhotonView().ViewID >= 1000 & player.GetPhotonView().ViewID <= 2000)
        {
            playerNumber = 0;
            //player.GetComponent<SpriteRenderer>().color = Color.red;
            //player.transform.position = points[0].transform.position;
           
            PV.RPC("RPC_Function1", RpcTarget.AllBuffered);



        }
        else if (player.GetPhotonView().ViewID >= 2000 & player.GetPhotonView().ViewID <= 3000)
        {
            playerNumber = 1;
            //player.GetComponent<SpriteRenderer>().color = Color.green;
            //player.transform.position = points[1].transform.position;
          
            PV.RPC("RPC_Function1", RpcTarget.AllBuffered);

        }
        else if (player.GetPhotonView().ViewID >= 3000 & player.GetPhotonView().ViewID <= 4000)
        {
            playerNumber = 2;
            //player.GetComponent<SpriteRenderer>().color = Color.blue;
           // player.transform.position = points[2].transform.position;
            
            PV.RPC("RPC_Function1", RpcTarget.AllBuffered);

        }
    }

    */

    public void SetOpinion(string nameTheme)
    {
        

        if (nameTheme == "1")
        {
            if (numberChooseTheme[0].text.Contains(PhotonNetwork.NickName))
            {
                if (makeChoice != 1)
                {
                    return;
                }
                RemoveNickName(numberChooseTheme[0].text, 0);
                PV.RPC("RPC_Function", RpcTarget.AllBuffered, numberChooseTheme[0].text, 1);
                return;
            }
            else if (makeChoice == 0)
            {
                numberChooseTheme[0].text = numberChooseTheme[0].text + "\n" + PhotonNetwork.NickName;
                PV.RPC("RPC_Function", RpcTarget.AllBuffered, numberChooseTheme[0].text, 1);
                makeChoice = 1;
                makedCh = true;
            }
            

        }
        else if (nameTheme == "2")
        {
            if (numberChooseTheme[1].text.Contains(PhotonNetwork.NickName))
            {
                if (makeChoice != 2)
                {
                    return;
                }
                RemoveNickName(numberChooseTheme[1].text, 1);
                PV.RPC("RPC_Function", RpcTarget.AllBuffered, numberChooseTheme[1].text, 2);
                return;
            }
            else if (makeChoice == 0)
            {
                numberChooseTheme[1].text = numberChooseTheme[1].text + "\n" + PhotonNetwork.NickName;
                PV.RPC("RPC_Function", RpcTarget.AllBuffered, numberChooseTheme[1].text, 2);
                makeChoice = 2;
                makedCh = true;
            }
            
        }
        else if (nameTheme == "3")
        {
            
            if (numberChooseTheme[2].text.Contains(PhotonNetwork.NickName))
            {
                if (makeChoice != 3)
                {
                    return;
                }
                RemoveNickName(numberChooseTheme[2].text, 2);
                PV.RPC("RPC_Function", RpcTarget.AllBuffered, numberChooseTheme[2].text, 3);
                return;
            }
            else if (makeChoice == 0)
            {
                numberChooseTheme[2].text = numberChooseTheme[2].text + "\n" + PhotonNetwork.NickName;
                PV.RPC("RPC_Function", RpcTarget.AllBuffered, numberChooseTheme[2].text, 3);
                makeChoice = 3;
                makedCh = true;
            }
            
        }


    }

    private void RemoveNickName(string str, int count)
    {
        
        int n = str.IndexOf(PhotonNetwork.NickName);
        str = str.Remove(n-1, PhotonNetwork.NickName.Length+1);
        numberChooseTheme[count].text = str;
        makeChoice = 0;
        makedCh = false;
    }

    [PunRPC]
    void RPC_Function(string text, int count)
    {
        if (count == 1)
        {
            numberChooseTheme[0].text = text;
        }
        else if (count == 2)
        {
            numberChooseTheme[1].text = text;
        }
        else if (count == 3)
        {
            numberChooseTheme[2].text = text;
        }
        
    }

    private void WaitingForMorePlayers()
    {
        string tempTimer = string.Format("{0:00}", timerToStartGame);
        timerToStartDisplay.text = tempTimer;
        if (PhotonNetwork.IsMasterClient)
            PV.RPC("RPC_SendTimer", RpcTarget.Others, timerToStartGame);
        timerToStartGame -= Time.deltaTime;

        if (timerToStartGame <= 0f)
        {
            PV.RPC("RPC_ControllChoice", RpcTarget.Others, makedCh);
            Debug.Log("RPC !!! makedCh = " + makedCh + "---" + PV.ViewID);
            //StartGame();
        }
    }

    [PunRPC]
    private void RPC_SendTimer(float timeIn)
    {
        timerToStartGame = timeIn;
        
    }

    [PunRPC]
    private void RPC_ControllChoice(bool madeCh)
    {
        
        
        Debug.Log("RPC madeCh = " + madeCh + "---" + PV.ViewID);
        if (madeCh == makedCh & madeCh == true)
        {
                StartGame();
        }
        else
        {
            timerToStartGame = fullGameTimer;
            return;
        }

    }

    public void StartGame()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        PhotonNetwork.LoadLevel(themesSceneIndex);
    }
}
