using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchingArea : MonoBehaviour
{
    IEnumerator Catching()
    {
        while (PlayerController.instance.isCatching && PlayerController.instance.life >= PlayerController.instance.catchingTimeStep)
        {
            PlayerController.instance.life -= PlayerController.instance.catchingTimeStep;
            yield return new WaitForSeconds(1);
        }

        while (!PlayerController.instance.isCatching && PlayerController.instance.life <= (100 - PlayerController.instance.catchingTimeStep))
        {
            PlayerController.instance.life += PlayerController.instance.catchingTimeStep;
            yield return new WaitForSeconds(3);
        }
    }


    private void OnTriggerStay(Collider other) 
    {
        // IF Cops Enter in the Player's Catching Area //
        if (!PlayerController.instance.isStarGet && !PlayerController.instance.isCatching) 
        {
            switch(other.gameObject.tag) 
            {
                case "Enemy" :
                    PlayerController.instance.isCatching = true;
                    StartCoroutine(Catching());
                    break;

                default:
                    break;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        // If Player has escaping to the COP //
        if (PlayerController.instance.isCatching) 
        {
            switch(other.gameObject.tag) 
            {
                case "Enemy" :
                    PlayerController.instance.isCatching = false;
                    StartCoroutine(Catching());
                    break;

                default:
                    break;
            }
        }
    }
}
