using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Bomb,
    Booster,
    Coin
}



public class ItemEffect : MonoBehaviour
{
    public ItemType itemType;

    private PlayerController playerController;


    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch(itemType)
            {
                // TODO
                /*
                 * Need Apply the Pooling OBJ
                */

                case ItemType.Bomb:
                    playerController.bomb = 3;          // Bomb Item Get
                    Debug.Log("Bomb GET!");
                    playerController.ItemBomb();
                    Destroy(gameObject);        // TODO
                    break;

                case ItemType.Booster:
                    playerController.isBooster = true;  // Booster ON
                    Debug.Log("Booster ON!");
                    playerController.ItemBooster();
                    Destroy(gameObject);        // TODO
                    break;

                case ItemType.Coin:
                    playerController.coin += 1;         // Coin += 1
                    Debug.Log("Coin GET!");
                    Destroy(gameObject);        // TODO
                    break;
            }
        }
    }
}