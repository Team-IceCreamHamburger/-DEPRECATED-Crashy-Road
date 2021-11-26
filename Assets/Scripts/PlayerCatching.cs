using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatching : MonoBehaviour
{
    public PlayerController player;
    public int catchingTimeStep;



    void FixedUpdate()
    {
        gameObject.transform.position = player.transform.position;
    }


    private void OnTriggerStay(Collider other)
    {
        // IF COPs Enter in the Player's Catching area //
        if (other.gameObject.tag == "Enemy" && !player.isCatching && !player.isStarGet)
        {
            player.isCatching = true;
            StartCoroutine(Catching());
        }
    }


    private void OnTriggerExit(Collider other)
    {
        // If Player has escaping to the COP //
        if (other.gameObject.tag == "Enemy" && player.isCatching)
        {
            player.isCatching = false;
            StartCoroutine(Catching());
        }
    }


    IEnumerator Catching()
    {
        while (player.isCatching && player.life >= catchingTimeStep)
        {
            player.life -= catchingTimeStep;
            yield return new WaitForSeconds(1);
        }

        while (!player.isCatching && player.life <= (100 - catchingTimeStep))
        {
            player.life += catchingTimeStep;
            yield return new WaitForSeconds(3);
        }
    }
}
