using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviour
{
    public void StartPlayeGame()
    {
        StartCoroutine(WaitSomeTime());
        
    }

    IEnumerator WaitSomeTime()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(2);
    }
}
