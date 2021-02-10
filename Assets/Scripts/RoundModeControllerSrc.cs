using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoundModeControllerSrc : MonoBehaviour
{
    [SerializeField]
    private Text displayCompletelyPl;

    [SerializeField]
    int countMode;

    [SerializeField]
    private Text textQuestion;

    private List<string> NamePlayerCompletedtask = new List<string>(); 

    private PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        Debug.Log(NamePlayerCompletedtask.Count);
    }

    private void Update()
    {
        WaitingCompletedTask();
    }

    public void СompletedTask()
    {
        
        PV.RPC("RPC_Completed", RpcTarget.AllBuffered, PhotonNetwork.NickName);
    }

    private void WaitingCompletedTask()
    {
        displayCompletelyPl.text = NamePlayerCompletedtask.Count + " / " + PhotonNetwork.PlayerList.Length;

        if (NamePlayerCompletedtask.Count == PhotonNetwork.PlayerList.Length)
        {
            //Debug.Log("Сработало");
        }

        
    }

    [PunRPC]
    private void RPC_Completed(string name)
    {
        if (NamePlayerCompletedtask.Contains(name))
        {
            Debug.Log("Win1");
            return;
        }
        NamePlayerCompletedtask.Add(name);
    }
}
