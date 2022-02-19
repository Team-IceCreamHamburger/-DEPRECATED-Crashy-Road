using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [HideInInspector] public GameObject centerOfMass;
    [HideInInspector] public float speedMulti;          // Boost Item Effect
    [HideInInspector] public bool isItemGet;
    [HideInInspector] public bool isStarGet;      // Item; Is Player Get the Star?    

    public int life;            // Player Life
    public int score;           // Score Count
    public float moveSpeed;     // Player Move Speed
    public float rotateSpeed;   // Player Rotate Speed

    public int catchingTimeStep;
    public bool isCatching = false;

    private Rigidbody playerRb;
    private int itemIndex;
    private int topSpeed = 100;
    private float hAxis;
    private float vAxis;
    private float currentSpeed = 0;
    private float pitch = 0;


    private void Init() 
    {
        if (instance == null) 
        {
            instance = this;
        }

        isItemGet = false;  // Item Reset
        isCatching = false;

        playerRb = gameObject.GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.localPosition;

        speedMulti = 1;

        // Wheels Torque ON //
        foreach (WheelCollider w in GetComponentsInChildren<WheelCollider>())
        {
            w.motorTorque = 0.000001f;
        }        
    }


    void Awake()
    {
        Init();
    }


    void FixedUpdate()
    {
        PlayerMove();
        PlayerDeath();
        EngineSound();
    }


    private void PlayerDeath()
    {
        if (life <= 0) 
        {
            UIController.instance.GameOver();
            gameObject.SetActive(false);
        }
    }


    private void PlayerMove()
    {
        // Axis Set //
        vAxis = Input.GetAxis("Vertical");
        hAxis = Input.GetAxis("Horizontal");

        // Player Move //
        playerRb.AddRelativeForce(Vector3.back * vAxis * moveSpeed * speedMulti);

        // Player Rotate //
        playerRb.angularVelocity = Vector3.zero;

        if (playerRb.velocity.magnitude > 0.5f) // IF Car is Moving
        {
            transform.Rotate(Vector3.up * hAxis * rotateSpeed * speedMulti * Time.deltaTime);
        }
    }


    private void EngineSound()
    {
        currentSpeed = playerRb.velocity.magnitude * 3.6f;
        pitch = currentSpeed / topSpeed;

        transform.GetComponent<AudioSource>().pitch = pitch;
    }


    private void ItemGetted(Item item, Collider other) 
    {
        if (!isItemGet) 
        {
            ItemController.instance.SetPointIndex(other.GetComponent<ItemIndex>().index);
            ItemController.instance.ItemGet(item);   // Booster GET
            Destroy(other.gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // IF Player fall in the water; GAMEOVER! //
        if (other.gameObject.tag == "Finish")   
        {
            life = 0;
        }

        // IF, Player Get The Item //
        switch (other.gameObject.tag)
        {
            case "Item_Booster":
                ItemGetted(Item.Booster, other);    
                break;

            case "Item_Coin":
                ItemGetted(Item.Coin, other);   
                break;

            case "Item_Star":
                ItemGetted(Item.Star, other);
                break;

            default:
                break;
        }
    }
}