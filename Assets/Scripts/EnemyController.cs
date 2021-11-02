using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int life;
    [SerializeField] private float exRotSpeed;
    
    private GameObject player;
    private NavMeshAgent agent;
    private Rigidbody enemyRb;
    private Vector3 velocity = Vector3.zero;
    private Vector3 lookRot;
    private bool isStop;



    private void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");

        agent.updatePosition = false;
        agent.updateRotation = false;
    }


    void FixedUpdate()
    {
        LookPlayer();
        StartCoroutine(ChasePlayer());
    }


    private void LookPlayer()
    {
        if (!isStop)
        {
            lookRot = agent.steeringTarget - gameObject.transform.position;
            transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(lookRot), exRotSpeed * Time.deltaTime);
        }
    }


    IEnumerator ChasePlayer()
    {
        if (!isStop)
        {
            agent.SetDestination(player.transform.position);    // agent Move
            transform.position = Vector3.SmoothDamp(gameObject.transform.position, agent.nextPosition, ref velocity, 0.1f); // HELL YEAH!!! DAMM YOU HORRIBLE BUG, YOU FIRED!!! 'A')!!!
        }

        yield return null;
    }




    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isStop = true;
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isStop = false;
        }
    }




}