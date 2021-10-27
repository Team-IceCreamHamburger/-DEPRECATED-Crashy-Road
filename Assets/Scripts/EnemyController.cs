using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject navPin;
    [SerializeField] private GameObject player;
    [SerializeField] private int life;
    [SerializeField] private float exRotSpeed;

    private NavMeshPath path;
    private NavMeshAgent agent;
    private Rigidbody enemyRb;
    private Vector3 lookRot;


    private void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
        agent   = GetComponent<NavMeshAgent>();
        
        path = new NavMeshPath();

        agent.updateRotation = false;

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        enemyRb.velocity = Vector3.zero;
        enemyRb.angularVelocity = Vector3.zero;

        StartCoroutine("PlayerTracking");

        StartCoroutine("FallInWater");
        

        // agent move Dir
        lookRot = agent.desiredVelocity;
        // Quaternion cal
        Quaternion targetAngle = Quaternion.LookRotation(lookRot);
        // Enemy Rotate
        transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, Time.deltaTime * exRotSpeed);    
    }


    IEnumerator FallInWater()
    {
        Ray ray;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down * 1, out hit, 15))
        {
            if (hit.transform.tag == "Finish")
            {
                agent.isStopped = true;
                yield return new WaitForSeconds(0.2f);
                gameObject.SetActive(false);
            }
        }
        yield return null;
    }


    IEnumerator PlayerTracking()
    {
        agent.CalculatePath(player.transform.position, path);
        agent.SetPath(path);

        navPin.transform.position = player.transform.position;

        yield return null;
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("!");
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            agent.isStopped = false;
        }

    }

}