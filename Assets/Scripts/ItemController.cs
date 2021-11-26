using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Item
{
    Star,
    Booster,
    Coin
}


public class ItemController : MonoBehaviour
{
    [HideInInspector] public int pointIndex;

    public Item spawnItem;
    public int spawnTime;
    public int starTime;
    public int boostTime;
    public int coin;
    public bool[] isPointFull;
    public Image gaugeBar;
    public GameObject player;
    public GameObject[] itemsIMG;
    public GameObject[] Items;
    public GameObject[] ItemSpawnPoints;

    private int spawnTimeTmp;
    private int starTimeTmp;
    private int boostTimeTmp;
    private int itemIndex;
    private AudioSource itemGetAudio;
    private PlayerController playerController;



    void Awake()
    {
        playerController = player.GetComponent<PlayerController>();
        itemGetAudio = GetComponent<AudioSource>();
        isPointFull = new bool[ItemSpawnPoints.Length];

        spawnTimeTmp = spawnTime;
        starTimeTmp = starTime;
        boostTimeTmp = boostTime;
    }


    void Start()
    {
        StartCoroutine(ItemRandomSpawn());
    }


    public void ItemGet(Item item, int itemIndex)
    {
        switch (item)
        {
            case Item.Star:
                StartCoroutine(ItemStarEf());       // Star Get
                isPointFull[itemIndex] = false;     // Item Spawn Point Reset
                break;

            case Item.Booster:
                StartCoroutine(ItemBoosterEf());    // Booster Get
                isPointFull[itemIndex] = false;     // Item Spawn Point Reset
                break;

            case Item.Coin:
                ItemCoinEf();       // Coin Get
                isPointFull[itemIndex] = false;  // Item Spawn Point Reset
                break;
        }
    }


    IEnumerator ItemBoosterEf()
    {
        playerController.isBoosterGet = true;

        itemGetAudio.Play();
        itemsIMG[0].SetActive(true);
        playerController.speedMulti = 2;    // BOOST ON

        while(boostTime > 0)        // Timer Start
        {
            boostTime -= 1;
            yield return new WaitForSeconds(1);
        }

        boostTime = boostTimeTmp;   // Timer End

        itemsIMG[0].SetActive(false);
        playerController.speedMulti = 1;    // BOOST OFF

        playerController.isBoosterGet = false;
    }


    IEnumerator ItemStarEf()
    {
        playerController.isStarGet = true;
        
        itemGetAudio.Play();
        itemsIMG[1].SetActive(true);
        gaugeBar.color = new Color(0.8666667f, 0.854902f, 0);

        while (starTime > 0)         // Timer Start
        {
            starTime -= 1;
            yield return new WaitForSeconds(1);
        }

        starTime = starTimeTmp;     // Time End

        itemsIMG[1].SetActive(false);
        gaugeBar.color = new Color(0, 0.8666667f, 0.05882353f);

        playerController.isStarGet = false;
    }


    private void ItemCoinEf()
    {
        playerController.isCoinGet = true;

        itemGetAudio.Play();
        playerController.score += coin;

        playerController.isCoinGet = false;
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