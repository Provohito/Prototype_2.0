using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;



public class PlayerControllerSrc : MonoBehaviourPun
{
    [SerializeField]
    private Text names;
    


    private void Start()
    {
        if (photonView.IsMine)
        {
            names.text = "You";
            return;
        }

        SetName();
        
    }
    
    private void SetName() => names.text = photonView.Owner.NickName;
    
    
}
