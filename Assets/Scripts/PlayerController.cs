using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public GameObject centerOfMass;
    [HideInInspector] public GameObject bombObj;

    [SerializeField] private float moveSpeed;   // Player Move Speed
    [SerializeField] private float rotateSpeed; // Player Rotate Speed

    public int topSpeed = 100;
    public int life = 3;        // Player Life
    public int coin = 0;        // Coint Count
    public int boostTimer;
    public bool isBombGet;      // Item; Is Player Get the Bomb?
    public bool isBoosterGet;   // Item; Is Player Get the Booster?
    public bool isCoinGet;      // Item; Is Player Get the Coin?

    private Rigidbody playerRb;
    private ItemController itemController;
    private UIController uiController;
    private int boostTimerTmp;
    private int itemIndex;
    private float speedMulti;   // Boost Item Effect
    private float hAxis;
    private float vAxis;
    private float currentSpeed = 0;
    private float pitch = 0;
    private bool isCoEntered;
    private Item item;


    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        itemController = GameObject.Find("ItemController").GetComponent<ItemController>();
        uiController = GameObject.Find("UIController").GetComponent<UIController>();

        playerRb.centerOfMass = centerOfMass.transform.localPosition;

        speedMulti = 1;
        boostTimerTmp = boostTimer;

        // Wheels Torque ON //
        foreach (WheelCollider w in GetComponentsInChildren<WheelCollider>())
        {
            w.motorTorque = 0.000001f;
        }
    }


    void FixedUpdate()
    {
        PlayerMove();
        EngineSound();
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


    private void ItemGet(Item item)
    {
        switch (item)
        {
            case Item.Bomb:
                //Debug.Log("DEL point Index: " + itemController.pointIndex);
                isBombGet = true;   // Bomb Get
                itemController.isPointFull[itemIndex] = false;  // Item Spawn Point Reset
                break;

            case Item.Booster:
                //Debug.Log("DEL point Index: " + itemController.pointIndex);
                isBoosterGet = true;   // Bomb Get
                itemController.isPointFull[itemIndex] = false;  // Item Spawn Point Reset
                break;

            case Item.Coin:
                //Debug.Log("DEL point Index: " + itemController.pointIndex);
                isCoinGet = true;   // Bomb Get
                itemController.isPointFull[itemIndex] = false;  // Item Spawn Point Reset
                break;
        }
    }


    private void ItemUse()
    {
        // If, Player Has The Item; Bomb, Booster, Coin //
        if (isBombGet)
        {
            ItemBombEf();
        }

        if (isBoosterGet)
        {
            ItemBoosterEf();
        }

        if (isCoinGet)
        {
            ItemCoinEF();
        }
    }


    private void ItemCoinEF()
    {
        isCoinGet = false;

        this.coin += 1;

        // TODO //
        /*
         * UI Update
         * SFX Play
        */
    }



    private void ItemBombEf()
    {
        if (Input.GetKeyDown(KeyCode.Space))    // Bomb Set on the player's pos.
        {
            isBombGet = false;
            //Instantiate(bombObj, gameObject.transform.position, Quaternion.identity);
        }

        // TODO //
        /*
         * UI Update
         * SFX Play
        */
    }


    private void ItemBoosterEf()
    {
        speedMulti = 2;     // Speed UP

        if (!isCoEntered)
        {
            StartCoroutine(BoostTimer());
        }

        // TODO //
        /*
         * UI Update
         * SFX Play
        */
    }


    IEnumerator BoostTimer()
    {
        isCoEntered = true;

        while (boostTimer >= 0)
        {
            boostTimer -= 1;
            yield return new WaitForSeconds(1);
        }

        speedMulti = 1;     // Speed Reset
        isBoosterGet = false;

        isCoEntered = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        // IF Player fall in the water; GAMEOVER! //
        if (other.gameObject.tag == "Finish")   
        {
            uiController.GameOver();
            gameObject.SetActive(false);
        }

        // IF, Player Get The Item //
        switch (other.gameObject.name)   // Item Get
        {
            case "Item_Bomb(Clone)":
                Debug.Log("Bomb GET!");
                itemIndex = other.GetComponent<ItemIndex>().index;
                ItemGet(Item.Bomb);      // Bomb GET
                Destroy(other.gameObject);
                break;

            case "Item_Booster(Clone)":
                Debug.Log("Booster GET!");
                itemIndex = other.GetComponent<ItemIndex>().index;
                ItemGet(Item.Booster);   // Booster GET
                Destroy(other.gameObject);
                break;

            case "Item_Coin(Clone)":
                Debug.Log("Coin GET!");
                itemIndex = other.GetComponent<ItemIndex>().index;
                ItemGet(Item.Coin);      // Coin GET
                Destroy(other.gameObject);
                break;
        }
    }
}