using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIndex : MonoBehaviour
{
    public int index;

    private ItemController itemController;



    void Start()
    {
        itemController = GameObject.Find("ItemController").GetComponent<ItemController>();

        index = itemController.pointIndex;
    }
}
