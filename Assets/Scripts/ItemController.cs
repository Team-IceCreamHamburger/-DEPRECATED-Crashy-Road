using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Items
{
    Bomb,
    Booster,
    Coin,
    Heal,
    Shield,
}


public class ItemController : MonoBehaviour
{
    public Items item;
    public GameObject player;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = player.GetComponent<PlayerController>();
    }


    // TODO //
    /*
     * Item Random Spawn
    */
}
