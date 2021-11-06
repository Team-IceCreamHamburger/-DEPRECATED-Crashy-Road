using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public AudioClip[] BGM;
    public List<GameObject> enemies;
    public GameObject enemyObj;
    public GameObject player;
    public Transform spawnPoint;
    public float enemySpawnRate;
    public int poolAmount;

    private bool isEntered;
    private AudioSource bgmPlayer;


    void Start()
    {
        bgmPlayer = GetComponent<AudioSource>();
        enemies = new List<GameObject>();           // Create a new List of GameObjects for Pooling

        EnemyPooler(enemyObj);  // Enemy Pooling
        BGMController();        // BGM Play
    }


    void Update()
    {
        StartCoroutine(EnemySpawner());     // Enemy Spawn Control
    }


    IEnumerator EnemySpawner()
    {
        if (player.activeSelf && !isEntered)   // If, Player is Still Alive
        {
            isEntered = true;                                           // Coroutine Entered

            // Search in the PooledObjects List
            for (int i = 0; i < enemies.Count; i++)
            {
                // IF, the pooled Obj. is NOT Activate, It can uses.
                if (!enemies[i].activeInHierarchy)
                {
                    GameObject cop = enemies[i];
                    cop.transform.position = spawnPoint.position;               // Cop Spawn at the Barricade Pos
                    cop.SetActive(true);
                    yield return new WaitForSeconds(enemySpawnRate);            // Cop Spawn CoolTime
                }
            }

            isEntered = false;                                          // Coroutine Escape
        }

        yield return null;
    }


    private void EnemyPooler(GameObject poolObj)
    {
        // Input GameObjects into the List for pooling
        for (int i = 0; i < poolAmount; i++)
        {
            GameObject obj = Instantiate(enemyObj);     // object Instantiate
            obj.SetActive(false);
            enemies.Add(obj);                           // List ADD
            obj.transform.SetParent(this.transform);    // Set as child. of GameController
        }
    }


    private void BGMController()
    {
        int indx = Random.Range(0, BGM.Length); // BGM index Random Select

        bgmPlayer.clip = BGM[indx];             // BGM Audio Clip SET
        bgmPlayer.Play();                       // BGM Start
    }




}
