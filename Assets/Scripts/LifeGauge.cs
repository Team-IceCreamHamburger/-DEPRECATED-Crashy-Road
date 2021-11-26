using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LifeGauge : MonoBehaviour
{
    public Image mask;
    public PlayerController playerController;


    // Update is called once per frame
    void Update()
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (playerController.life * 4.8f));
    }
}
