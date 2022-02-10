using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoSend : MonoBehaviour
{
    public void playerSet() 
    {
        ObjectInfo.instance.player = Preview.instance.getCar();
    }


    public void mapSet() 
    {
        ObjectInfo.instance.map = Preview.instance.getMap();
    }
}
