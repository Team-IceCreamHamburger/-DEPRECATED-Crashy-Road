using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //[SerializeField] private int life;
    [SerializeField] private float exRotSpeed;

    private GameController gameController;
    private GameObject player;
    private NavMeshAgent agent;
    private Rigidbody enemyRb;
    private Vector3 velocity = Vector3.zero;
    private Vector3 lookRot;
    public bool isHit;
    //private int lifeTmp;


    void Awake()
    {
        //lifeTmp = life;
        isHit = false;

        enemyRb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        agent.updatePosition = false;
        agent.updateRotation = false;
    }


    void FixedUpdate()
    {
        LookPlayer();
        StartCoroutine(ChasePlayer());
    }


    void Update()
    {
        Hit();
        //Died();
    }

    /*
    private void Hit()
    {
        if (isHit)
        {
            this.life -= 1;
        }
    }
    */

    
    private void Hit()
    {
        if (isHit)
        {
            StartCoroutine(Crash());
            //this.gameObject.SetActive(false);
        }
        //life = lifeTmp;
    }


    private void LookPlayer()
    {
        if (!isHit && player.activeSelf)
        {
            lookRot = agent.steeringTarget - gameObject.transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookRot), exRotSpeed * Time.deltaTime);
        }
    }


    IEnumerator Crash()
    {
        yield return new WaitForSeconds(gameController.enemySpawnRate);
        isHit = false;
        this.gameObject.SetActive(false);
    }


    IEnumerator ChasePlayer()
    {
        if (!isHit && player.activeSelf)
        {
            agent.SetDestination(player.transform.position);    // agent Move
            transform.position = Vector3.SmoothDamp(transform.position, agent.nextPosition, ref velocity, 0.1f); // HELL YEAH!!! DAMM YOU HORRIBLE BUG, YOU FIRED!!! 'A')!!!
        }

        yield return null;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("CRASH!!");
            isHit = true;
        }
    }

    /*
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isHit = false;
        }
    }
    */
    
}