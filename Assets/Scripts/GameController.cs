using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Enemy;
    public Vector3[] spawnPoint;

    [SerializeField] private float spawnRate;

    private int point;
    public bool isSpawned;


    private void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (!isSpawned)
        {
            point = Random.Range(0, spawnPoint.Length);
            StartCoroutine(EnemySpawn(point));
        }
    }


    IEnumerator EnemySpawn(int point)
    {
        Instantiate(Enemy, spawnPoint[point], Quaternion.identity);
        isSpawned = true;
        yield return new WaitForSeconds(spawnRate);
        isSpawned = false;
    }


}
