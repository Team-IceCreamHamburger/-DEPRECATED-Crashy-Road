using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item
{
    Bomb,
    Booster,
    Coin
}


public class ItemController : MonoBehaviour
{
    [HideInInspector] public int pointIndex;

    public Item spawnItem;
    public int spawnTime;
    public bool[] isPointFull;
    public GameObject[] Items;
    public GameObject[] ItemSpawnPoints;
    

    private GameObject player;
    private int spawnTimeTmp;
    private int itemIndex;


    void Awake()
    {
        player = GameObject.Find("Player");
        isPointFull = new bool[ItemSpawnPoints.Length];
        spawnTimeTmp = spawnTime;
    }

    void Start()
    {
        StartCoroutine(ItemRandomSpawn());
    }


    IEnumerator ItemRandomSpawn()
    {
        while (true)
        {
            if (spawnTime <= 0)     // Time OUT; Item Spawn
            {
                // Item Random Select && Spawn //
                pointIndex = Random.Range(0, ItemSpawnPoints.Length);   // Spawn Point Check; Is Point Empty?

                if (!isPointFull[pointIndex])   // If Empty, Item Spawn at Point
                {
                    Debug.Log("point Index: " + pointIndex);
                    isPointFull[pointIndex] = true;
                    itemIndex = Random.Range(0, System.Enum.GetValues(typeof(Item)).Length);   // Item Random Select
                    Instantiate(Items[itemIndex], ItemSpawnPoints[pointIndex].transform.position, Quaternion.identity);
                }

                spawnTime = spawnTimeTmp;   // Spawn Timer Reset
            }
            else
            {
                spawnTime -= 1;
                yield return new WaitForSeconds(1);
            }
        }
    }
}