using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int life = 3;    // Player Life
    public int bomb = 0;    // Item; Bomb Count
    public bool isShield;   // Item; Shield On/Off
    public bool isBooster;  // item; Booster On/Off
    public bool isKilled;   // Is Player Killed?

    [SerializeField] private GameObject centerOfMass;
    [SerializeField] private float moveSpeed;   // Player Move Speed
    [SerializeField] private float rotateSpeed; // Player Rotate Speed

    private Rigidbody playerRb;
    private float hAxis;
    private float vAxis;


    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.localPosition;

        // Wheels Torque ON //
        foreach (WheelCollider w in GetComponentsInChildren<WheelCollider>())
        {
            w.motorTorque = 0.000001f;
        }
    }


    void FixedUpdate()
    {
        PlayerControll();
    }


    private void PlayerControll()
    {
        // Axis Set //
        vAxis = Input.GetAxis("Vertical");
        hAxis = Input.GetAxis("Horizontal");

        // Player Move //
        playerRb.AddRelativeForce(Vector3.back * vAxis * moveSpeed);

        // Player Rotate //
        playerRb.angularVelocity = Vector3.zero;

        if (playerRb.velocity.magnitude > 0.5f) // IF Car is Moving
        {
            transform.Rotate(Vector3.up * hAxis * rotateSpeed * Time.deltaTime);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")   // IF Player fall in the water; GAMEOVER!
        {
            isKilled = true;
            gameObject.SetActive(false);
            Debug.Log("GameOver!");
        }
    }
}