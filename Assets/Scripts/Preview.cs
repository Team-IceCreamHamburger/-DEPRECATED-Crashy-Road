using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour
{
    public static Preview instance;

    public GameObject[] objects;

    private int index;


    private void Init() 
    {
        index = 0;
    }


    void Awake() 
    {
        if(instance == null) {
            instance = this;
        }

        Init();
    }

    
    private void ListRest() 
    {
        foreach(GameObject obj in objects) 
        {
            obj.SetActive(false);
        }
    }


    public void Next() 
    {
        ListRest();

        index += 1;

        if (index >= objects.Length) 
        {
            index = 0;
        }
        
        objects[index].SetActive(true);
    }


    public void Prev() 
    {
        ListRest();

        index -= 1;
        
        if (index <= -1) 
        {
            index = (objects.Length - 1);
        }

        objects[index].SetActive(true);
    }


    public string getCar()
    {
        return objects[index].name;
    }


    public string getMap()
    {
        return objects[index].name;
    }
}