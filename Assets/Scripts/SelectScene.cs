using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SelectScene : MonoBehaviour
{
    public void JumpToScene(string scene) {
        SceneManager.LoadScene(scene);
    }

}
