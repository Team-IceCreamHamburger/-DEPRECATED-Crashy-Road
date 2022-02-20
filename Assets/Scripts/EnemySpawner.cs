using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    public GameObject[] enemyObj;
    public Transform spawnPoint;
    public GameObject player;
    private List<GameObject> enemies;
    public float enemySpawnRate;

    private bool isEntered;
    private int poolAmount;
    


    private void Init()
    {
        if (instance == null) 
        {
            instance = this;
        }

        enemies = new List<GameObject>();   // Create a new List of GameObjects for Pooling
        isEntered = false;
        poolAmount = 30;
    }


    void Awake()
    {
        Init();
    }


    void Start()
    {
        EnemyPool(enemyObj[0]);                // Enemy Pooling
    }


    void Update()
    {
        StartCoroutine(EnemySpawn());       // Enemy Spawn Control
    }


    IEnumerator EnemySpawn()
    {
        if (player.activeSelf && !isEntered)   // If, Player is Still Alive
        {
            isEntered = true;                  // Coroutine Entered

            // Search in the PooledObjects List
            for (int i = 0; i < enemies.Count; i++)
            {
                // IF, the pooled Obj. is NOT Activate, It can uses.
                if (!enemies[i].activeInHierarchy)
                {
                    GameObject cop = enemies[i];
                    cop.transform.position = spawnPoint.position;       // Cop Spawn at the Barricade Pos
                    cop.SetActive(true);
                    yield return new WaitForSeconds(enemySpawnRate);    // Cop Spawn CoolTime
                }
            }

            isEntered = false;  // Coroutine Escape
        }

        yield return null;
    }


    private void EnemyPool(GameObject poolObj)
    {
        // Input GameObjects into the List for pooling
        for (int i = 0; i < poolAmount; i++)
        {
            GameObject obj = Instantiate(enemyObj[0]);     // object Instantiate
            obj.SetActive(false);
            enemies.Add(obj);                           // List ADD
            obj.transform.SetParent(this.transform);    // Set as child. of GameController
        }
    }
}
