using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SelectScene : MonoBehaviour
{
    public void JumpToScene(string scene) {
        SceneManager.LoadScene(scene);
    }


    /*
    public void ToLevel() {
        SceneManager.LoadScene("LevelSelect");
    }


    public void BackToMenu() {
        SceneManager.LoadScene("Title");
    }
    */
}
