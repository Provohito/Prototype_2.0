﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class DelayStartLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject delayStartBtn;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        delayStartBtn.SetActive(true);
    }

    public void DelayStart()
    {
        delayStartBtn.SetActive(false);
        SceneManager.LoadScene(1);
        Debug.Log("Delay Start");
    }
}
