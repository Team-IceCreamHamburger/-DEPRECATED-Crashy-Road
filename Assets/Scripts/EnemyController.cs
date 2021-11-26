using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private AudioClip siren;
    [SerializeField] private int cooltime;
    [SerializeField] private float exRotSpeed;

    private AudioSource sirenPlayer;
    private EnemySpawner enemySpawner;
    private GameObject player;
    private PlayerController playerController;
    private NavMeshAgent agent;
    private Rigidbody enemyRb;
    private Vector3 velocity = Vector3.zero;
    private Vector3 lookRot;
    private int hitCount;
    private bool isHit;



    void Awake()
    {
        sirenPlayer = GetComponent<AudioSource>();
        enemyRb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();

        isHit = false;
        hitCount = 0;
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
        SirenController();
    }

    
    private void Hit()
    {
        if (isHit)
        {
            StartCoroutine(Crash());
        }
    }


    private void LookPlayer()
    {
        if (!isHit && player.activeSelf)
        {
            lookRot = agent.steeringTarget - gameObject.transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookRot), exRotSpeed * Time.deltaTime);
        }
    }


    private void SirenController()
    {
        if (gameObject.activeSelf && !sirenPlayer.isPlaying)
        {
            sirenPlayer.clip = siren;
            sirenPlayer.loop = true;
            sirenPlayer.Play();
        }
    }


    IEnumerator Crash()
    {
        yield return new WaitForSeconds(cooltime);
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
            hitCount += 1;
            //Debug.Log("Hit Count: " + hitCount);
            isHit = true;
        }
    }
}