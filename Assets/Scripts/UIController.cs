using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject pauseWin;
    public GameObject overWin;
    public GameObject dChkWin;


    public void Awake()
    {
        pauseWin.SetActive(false);
        overWin.SetActive(false);
        dChkWin.SetActive(false);
    }


    public void GamePause()
    {
        /* // TODO //
         * Audio Volume Mute
        */ // END TODO END //

        Time.timeScale = 0;     // Game Pause ON
        pauseWin.SetActive(true);
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
        Application.Quit();
    }


    public void Close()
    {
        /* // TODO //
         * Pause Window Close
        */ // END TODO END //

        Time.timeScale = 1;
    }


    public void YES()
    {
        /* // TODO //
         * Yes Button Logic Implement
        */ // END TODO END //
    }


    public void NO()
    {
        /* // TODO //
         * NO Button Logic Implement
        */ // END TODO END //
    }


    public void GameOver()
    {
        Time.timeScale = 0;
        overWin.SetActive(true);
    }


    public void DoubleCheck()
    {
        dChkWin.SetActive(true);
    }
}