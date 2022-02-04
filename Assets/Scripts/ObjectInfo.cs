using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour
{
    public static ObjectInfo instance;
    public string player;
    public string map;


    
    void Awake() 
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);      
    }


    public void playerSet() 
    {
        player = Preview.instance.getCar();
    }


    public void mapSet() 
    {
        map = Preview.instance.getMap();
    }
}