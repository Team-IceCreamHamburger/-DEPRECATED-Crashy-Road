using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatching : MonoBehaviour
{
    public int catchingTimeStep;
    public bool isCatching;     // Is Player Catching?


    /*
    void FixedUpdate()
    {
        gameObject.transform.position = PlayerController.instance.gameObject.transform.position;
    }
    */


    private void Init() 
    {
        isCatching = false;
    }


    void Awake() {
        Init();
    }


    private void OnTriggerStay(Collider other)
    {
        // IF COPs Enter in the Player's Catching area //
        if (other.gameObject.tag == "Enemy" && !isCatching && !PlayerController.instance.isStarGet)
        {
            isCatching = true;
            StartCoroutine(Catching());
        }
    }


    private void OnTriggerExit(Collider other)
    {
        // If Player has escaping to the COP //
        if (other.gameObject.tag == "Enemy" && isCatching)
        {
            isCatching = false;
            StartCoroutine(Catching());
        }
    }


    IEnumerator Catching()
    {
        while (isCatching && PlayerController.instance.life >= catchingTimeStep)
        {
            PlayerController.instance.life -= catchingTimeStep;
            yield return new WaitForSeconds(1);
        }

        while (!isCatching && PlayerController.instance.life <= (100 - catchingTimeStep))
        {
            PlayerController.instance.life += catchingTimeStep;
            yield return new WaitForSeconds(3);
        }
    }
}
