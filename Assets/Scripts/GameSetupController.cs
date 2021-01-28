using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameSetupController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] points;

    private void Start()
    {
        CreatePlayer();
        
    }

  
    private void CreatePlayer()
    {
        
        GameObject player =  PhotonNetwork.Instantiate("player", Vector3.zero, Quaternion.identity);
        
        if (player.GetPhotonView().ViewID >= 1000 & player.GetPhotonView().ViewID <= 2000)
        {
            Debug.Log("Win");
            player.GetComponent<SpriteRenderer>().color = Color.red;
            player.transform.position = points[0].transform.position;

        }
        else if (player.GetPhotonView().ViewID >= 2000 & player.GetPhotonView().ViewID <= 3000)
        {
            Debug.Log("Win1");
            player.GetComponent<SpriteRenderer>().color = Color.green;
            player.transform.position = points[1].transform.position;
        }  
       

    }

    


}
