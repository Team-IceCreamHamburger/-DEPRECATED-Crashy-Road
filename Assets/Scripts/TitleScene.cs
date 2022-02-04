using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class TitleScene : MonoBehaviour
{
    public GameObject gameLogo;
    public GameObject dChkWin;
    public GameObject howWin;
    public GameObject[] titles;
    public AudioSource buttonSFX;


    void Awake()
    {
        int idx = Random.Range(0, 2);
        titles[idx].SetActive(true);
    }

    
    public void GameStart()
    {
        buttonSFX.Play();
        SceneManager.LoadScene("CarSelect");
    }
    

    public void HowToPlay()
    {
        buttonSFX.Play();
        gameLogo.SetActive(false);
        howWin.SetActive(true);
    }


    public void GameQuit()
    {
        buttonSFX.Play();
        DoubleCheck();
    }


    private void DoubleCheck()
    {
        dChkWin.SetActive(true);
    }


    public void YES()
    {
        buttonSFX.Play();
        Application.Quit();
    }


    public void NO()
    {
        buttonSFX.Play();
        dChkWin.SetActive(false);
    }


    public void WinClose()
    {
        buttonSFX.Play();
        gameLogo.SetActive(true);
        howWin.SetActive(false);
    }
}
