using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class StartLoadScript : MonoBehaviour
{
    [SerializeField]
    private Slider _loadSlider;

    float stepSlider = 0.1f;

    [SerializeField]
    private Text _persentText;

    string countPlayersOnline;

    [SerializeField]
    private Text textTv;

    void Update()
    {
        
        countPlayersOnline = PhotonNetwork.CountOfPlayersOnMaster.ToString() + "";
        Debug.Log(countPlayersOnline);
        textTv.text = "" + countPlayersOnline;

        _loadSlider.value += stepSlider * Time.deltaTime;
        _persentText.text = Mathf.Round(_loadSlider.value * 100) + " %"; 


        if (_loadSlider.value == 1)
        {
            NextScene();
        }
        GameObject[] rew = GameObject.FindGameObjectsWithTag("player");
        for (int i = 0; i < rew.Length; i++)
        {
            rew[i].transform.SetParent(this.transform);
        }
    }

    void NextScene()
    {
        
         Debug.Log("Win");
        
    }
    
}
