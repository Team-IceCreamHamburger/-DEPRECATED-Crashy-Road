using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class BGMController : MonoBehaviour
{
    public static BGMController instance;

    public AudioMixer mixer;
    public AudioSource bgmPlayer;
    public AudioClip[] BGM;




    private void Init() 
    {
        if (instance == null) {
            instance = this;
        }

        SetVol("master", 1f);       // Game Audio ON
    }


    void Awake()
    {
        Init();
    }


    void Start()
    {
        BGMPlay();        // BGM Play
    }


    // AUDIO MIXER
    public void SetVol(string mixerTg, float val)
    {
        mixer.SetFloat(mixerTg, Mathf.Log10(val) * 20);
    }


    private void BGMPlay()
    {
        int indx = Random.Range(0, BGM.Length); // BGM index Random Select

        bgmPlayer.clip = BGM[indx];             // BGM Audio Clip SET
        bgmPlayer.Play();                       // BGM Start
    }
}
