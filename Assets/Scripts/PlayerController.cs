using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    [HideInInspector] public GameObject centerOfMass;
    [HideInInspector] public GameObject bombObj;

    public Text scoreText;
    public int life;            // Player Life
    public int score;           // Score Count
    public int boostTimer;
    public float moveSpeed;     // Player Move Speed
    public float rotateSpeed;   // Player Rotate Speed
    public bool isStarGet;      // Item; Is Player Get the Star?
    public bool isBoosterGet;   // Item; Is Player Get the Booster?
    public bool isCoinGet;      // Item; Is Player Get the Coin?
    public bool isCatching;

    private Item item;
    private Rigidbody playerRb;
    private ItemController itemController;
    private UIController uiController;
    private int boostTimerTmp;
    private int itemIndex;
    private int topSpeed = 100;
    private float speedMulti;   // Boost Item Effect
    private float hAxis;
    private float vAxis;
    private float currentSpeed = 0;
    private float pitch = 0;
    


    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        itemController = GameObject.Find("ItemController").GetComponent<ItemController>();
        uiController = GameObject.Find("UIController").GetComponent<UIController>();

        playerRb.centerOfMass = centerOfMass.transform.localPosition;

        isCatching = false;

        speedMulti = 1;
        boostTimerTmp = boostTimer;

        // Wheels Torque ON //
        foreach (WheelCollider w in GetComponentsInChildren<WheelCollider>())
        {
            w.motorTorque = 0.000001f;
        }
    }


    void Start()
    {
        StartCoroutine(ScoreCount());
    }


    void FixedUpdate()
    {
        PlayerMove();
        EngineSound();
    }


    private void PlayerDeath()
    {
        uiController.GameOver();
        gameObject.SetActive(false);
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

        // Player Death //
        if (life <= 0)
        {
            PlayerDeath();
        }
    }


    private void EngineSound()
    {
        currentSpeed = playerRb.velocity.magnitude * 3.6f;
        pitch = currentSpeed / topSpeed;

        transform.GetComponent<AudioSource>().pitch = pitch;
    }


    IEnumerator ScoreCount()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            score += 1;
            scoreText.text = string.Format("{0:D9}", score);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // IF Player fall in the water; GAMEOVER! //
        if (other.gameObject.tag == "Finish")   
        {
            PlayerDeath();
        }

        // IF, Player Get The Item //
        switch (other.gameObject.name)
        {
            case "Item_Star(Clone)":
                itemIndex = other.GetComponent<ItemIndex>().index;
                itemController.ItemGet(Item.Star, itemIndex);      // Bomb GET
                Destroy(other.gameObject);
                break;

            case "Item_Booster(Clone)":
                itemIndex = other.GetComponent<ItemIndex>().index;
                itemController.ItemGet(Item.Booster, itemIndex);   // Booster GET
                Destroy(other.gameObject);
                break;

            case "Item_Coin(Clone)":
                itemIndex = other.GetComponent<ItemIndex>().index;
                itemController.ItemGet(Item.Coin, itemIndex);      // Coin GET
                Destroy(other.gameObject);
                break;
        }
    }
}