using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkController : MonoBehaviourPunCallbacks
{
    private string namePlayer;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        namePlayer = Random.Range(1,2000).ToString();
        PhotonNetwork.NickName = namePlayer;

    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server!");
    }
}
