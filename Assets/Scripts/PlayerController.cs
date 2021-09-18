using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int life = 3;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;

    private float hAxis;
    private float vAxis;
    private Rigidbody playerRb;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        vAxis = Input.GetAxis("Vertical");
        hAxis = Input.GetAxis("Horizontal");

        // Player Move //
        //transform.Translate(Vector3.back * vAxis * moveSpeed * Time.deltaTime);
        playerRb.AddRelativeForce(Vector3.back * vAxis * moveSpeed);

        // Player Rotate //
        if (vAxis != 0.0f)
        {
            transform.Rotate(Vector3.up * hAxis * rotateSpeed * Time.deltaTime);
        }
    }
}