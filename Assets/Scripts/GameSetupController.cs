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

    int playerNumber;

   


    private void Start()
    {
        PV = GetComponent<PhotonView>();
        CreatePlayer();
    }

  
    private void CreatePlayer()
    {
        
        player = PhotonNetwork.Instantiate("player", Vector3.zero, Quaternion.identity);

        if (player.GetPhotonView().ViewID >= 1000 & player.GetPhotonView().ViewID <= 2000)
        {
            playerNumber = 0;
            player.GetComponent<SpriteRenderer>().color = Color.red;
            player.transform.position = points[0].transform.position;
            //player.GetComponentInChildren<TextMeshPro>().text = PhotonNetwork.NickName;

        }
        else if (player.GetPhotonView().ViewID >= 2000 & player.GetPhotonView().ViewID <= 3000)
        {
            playerNumber = 1;
            player.GetComponent<SpriteRenderer>().color = Color.green;
            player.transform.position = points[1].transform.position;
            //player.GetComponentInChildren<TextMeshPro>().text = PhotonNetwork.NickName;      
        }
        else if (player.GetPhotonView().ViewID >= 3000 & player.GetPhotonView().ViewID <= 4000)
        {
            playerNumber = 2;
            player.GetComponent<SpriteRenderer>().color = Color.blue;
            player.transform.position = points[2].transform.position;
            //player.GetComponentInChildren<TextMeshPro>().text = PhotonNetwork.NickName;      
        }
    }

    public void SetOpinion(string nameTheme)
    {
        

        if (nameTheme == "1")
        {
            if (numberChooseTheme[0].text.Contains(PhotonNetwork.NickName))
            {
                return;
            }
            numberChooseTheme[0].text = numberChooseTheme[0].text + "\n" + PhotonNetwork.NickName;
            PV.RPC("RPC_Function", RpcTarget.AllBuffered, numberChooseTheme[0].text, 1);
            

        }
        else if (nameTheme == "2")
        {
            
        }
        else if (nameTheme == "3")
        {
            
        }


    }

    [PunRPC]
    void RPC_Function(string text, int count)
    {
        if (count == 1)
        {
            numberChooseTheme[0].text = text;
        }
        
    }
    


}
