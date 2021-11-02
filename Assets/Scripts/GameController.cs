using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<GameObject> enemies;
    public GameObject enemyObj;
    public Transform SpawnPoint;
    public GameObject player;
    public int poolAmount;
    public float enemySpawnRate;
    private bool isEntered;


    void Start()
    {
        // Create a new List of GameObjects for Pooling
        enemies = new List<GameObject>();

        EnemyPooler(enemyObj);
    }


    void Update()
    {
        StartCoroutine(EnemySpawner());
    }


    IEnumerator EnemySpawner()
    {
        if (player.activeInHierarchy && !isEntered)   // If, Player is Still Alive
        {
            isEntered = true;                                           // Coroutine Entered
            GameObject cop = GetPooledObj();                            // Pooling
            cop.transform.position = SpawnPoint.position;               // Cop Spawn at the Barricade Pos
            cop.SetActive(true);
            yield return new WaitForSeconds(enemySpawnRate);            // Cop Spawn CoolTime
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


    public GameObject GetPooledObj()
    {
        // Search in the PooledObjects List
        for (int i = 0; i < enemies.Count; i++)
        {
            // IF, the pooled Obj. is NOT Activate, It can uses.
            if (!enemies[i].activeInHierarchy)
            {
                return enemies[i];
            }
        }

        return null;
    }
}
