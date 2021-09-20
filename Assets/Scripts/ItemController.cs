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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log(gameObject.name);

            // Item effect acivate //
            switch (gameObject.GetComponent<ItemController>().item)
            {
                case Items.Bomb:
                    playerController.bomb += 1;
                    Destroy(gameObject);
                    break;
                case Items.Booster:
                    playerController.isBooster = true;
                    Destroy(gameObject);
                    break;
                case Items.Coin:
                    Destroy(gameObject);
                    break;
                case Items.Heal:
                    if (playerController.life < 3)
                    {
                        playerController.life += 1;
                        Destroy(gameObject);
                    }
                    break;
                case Items.Shield:
                    if (!playerController.isShield)
                    {
                        playerController.isShield = true;
                        Destroy(gameObject);
                    }
                    break;
            }
        }
    }
}
