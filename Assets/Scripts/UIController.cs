using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public static UIController instance;

    public GameObject gamePauseWin;
    public GameObject gameOverWin;
    public GameObject doubleCheckWin;
    public GameObject[] itemsIMG;
    public Image gaugeBar;
    public Image gaugeBarMask;
    public Text scoreText;



    private void Init() 
    {
        if (instance == null) 
        {
            instance = this;
        }

        Time.timeScale = 1;

        gamePauseWin.SetActive(false);
        gameOverWin.SetActive(false);
        doubleCheckWin.SetActive(false);
    }


    void Awake()
    {
        Init();
    }


    void Start()
    {
        StartCoroutine(ScoreCount());
    }


    IEnumerator ScoreCount()
    {
        while (true)
        {
            instance.scoreText.text = string.Format("{0}", PlayerController.instance.score);
            yield return new WaitForSeconds(1);
            PlayerController.instance.score += 1;
        }
    }
    // TODO //


    void Update() 
    {
        gaugeBarMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (PlayerController.instance.life * 4.8f));
    }



    public void ItemIcon(Item item, bool active) 
    {
        itemsIMG[(int)item].SetActive(active);
    }


    public void StarGauge(Color color) 
    {
        gaugeBar.color = color;
    }


    public void GamePause()
    {
        gamePauseWin.SetActive(true);
        Time.timeScale = 0;             // Game Pause ON
        BGMController.instance.SetVol("master", 0.0001f);      // Game Audio OFF
    }


    public void Close()
    {
        gamePauseWin.SetActive(false);
        Time.timeScale = 1;             // Game Pause OFF
        BGMController.instance.SetVol("master", 1f);           // Game Audio ON
    }


    public void Retry()
    {
        Time.timeScale = 1;             // Game Pause OFF
        SceneManager.LoadScene(1);      // Game Scene Re-Load
    }


    public void Title()
    {
        SceneManager.LoadScene(0);      // Title Scene Load
    }


    public void Quit()
    {
        DoubleCheck();
    }


    private void DoubleCheck()
    {
        doubleCheckWin.SetActive(true);
    }


    public void YES()
    {
        Application.Quit();
    }


    public void NO()
    {
        doubleCheckWin.SetActive(false);
    }


    public void GameOver()
    {
        gameOverWin.SetActive(true);
        Time.timeScale = 0;             // Game Pause ON
        BGMController.instance.SetVol("master", 0.0001f);      // Game Audio OFF
    }
}