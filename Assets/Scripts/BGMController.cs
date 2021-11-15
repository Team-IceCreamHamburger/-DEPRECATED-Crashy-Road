using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    public AudioClip[] BGM;
    private AudioSource bgmPlayer;


    void Awake()
    {
        bgmPlayer = GetComponent<AudioSource>();
    }


    void Start()
    {
        BGMPlay();        // BGM Play
    }


    private void BGMPlay()
    {
        int indx = Random.Range(0, BGM.Length); // BGM index Random Select

        bgmPlayer.clip = BGM[indx];             // BGM Audio Clip SET
        bgmPlayer.Play();                       // BGM Start
    }
}
