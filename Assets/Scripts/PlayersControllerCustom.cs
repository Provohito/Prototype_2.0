using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayersControllerCustom : MonoBehaviourPunCallbacks
{

    private PhotonView pv;
    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    
    void Update()
    {
        
    }

    void CreatePlayer()
    {
        
    }
}
