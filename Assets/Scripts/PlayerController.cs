using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int life = 3;
    public int bomb = 0;
    public bool isShield;
    public bool isBooster;

    [SerializeField] private GameObject centerOfMass;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;

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
        vAxis = Input.GetAxis("Vertical");
        hAxis = Input.GetAxis("Horizontal");

        // Player Move //
        playerRb.AddRelativeForce(Vector3.forward * vAxis * moveSpeed);

        // Player Rotate //
        playerRb.angularVelocity = Vector3.zero;
        if (playerRb.velocity.magnitude > 0.0f)
        {
            transform.Rotate(Vector3.up * hAxis * rotateSpeed * Time.deltaTime);
        }

        playerRb.angularVelocity = Vector3.zero;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")   // IF Player fall in the water
        {
            gameObject.SetActive(false);
            Debug.Log("GameOver!");
        }
    }
}