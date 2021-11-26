using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class UIController : MonoBehaviour
{
    public AudioMixer mixer;
    public GameObject pauseWin;
    public GameObject overWin;
    public GameObject dChkWin;

    void Awake()
    {
        SetVol("master", 1f);       // Game Audio ON
        pauseWin.SetActive(false);
        overWin.SetActive(false);
        dChkWin.SetActive(false);
    }


    private void SetVol(string mixerTg, float val)
    {
        mixer.SetFloat(mixerTg, Mathf.Log10(val) * 20);
    }


    public void GamePause()
    {
        pauseWin.SetActive(true);
        Time.timeScale = 0;             // Game Pause ON
        SetVol("master", 0.0001f);      // Game Audio OFF
    }


    public void Close()
    {
        pauseWin.SetActive(false);
        Time.timeScale = 1;             // Game Pause OFF
        SetVol("master", 1f);           // Game Audio ON
    }


    public void Retry()
    {
        Time.timeScale = 1;             // Game Pause OFF
        SceneManager.LoadScene(0);      // Game Scene Re-Load
    }


    public void Title()
    {
        /* // TODO //
         * Add Title Scene
        */ // END TODO END //

        SceneManager.LoadScene(1);      // Title Scene Load
    }


    public void Quit()
    {
        DoubleCheck();
    }


    private void DoubleCheck()
    {
        dChkWin.SetActive(true);
    }


    public void YES()
    {
        Application.Quit();
    }


    public void NO()
    {
        dChkWin.SetActive(false);
    }


    public void GameOver()
    {
        overWin.SetActive(true);
        Time.timeScale = 0;             // Game Pause ON
        SetVol("master", 0.0001f);      // Game Audio OFF
    }
}