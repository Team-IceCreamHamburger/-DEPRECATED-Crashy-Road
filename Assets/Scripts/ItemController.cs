using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Item
{
    Booster,
    Coin,
    Star
}


public class ItemController : MonoBehaviour
{
    public static ItemController instance;

    //public Item spawnItem;
    public GameObject[] Items;
    public GameObject[] ItemSpawnPoints;
    public int itemSpawnCoolTime;
    public int starTime;
    public int boostTime;
    public int coinTime;
    

    private AudioSource itemSFX;
    private int coinScore;
    private int itemSpawnCoolTimeTmp;
    private int starTimeTmp;
    private int boostTimeTmp;
    private int coinTimeTmp;
    private int itemIndex;
    private int pointIndex;
    private int randomPointIndex;
    private bool[] isPointFull;


    private void Init() 
    {
        if (instance == null) 
        {
            instance = this;
        }

        itemSFX = gameObject.GetComponent<AudioSource>();
        isPointFull = new bool[ItemSpawnPoints.Length];

        itemSpawnCoolTimeTmp = itemSpawnCoolTime;
        starTimeTmp = starTime;
        boostTimeTmp = boostTime;
        coinTimeTmp = coinTime;

        StartCoroutine(ItemRandomSpawn());
    }


    void Awake()
    {
        Init();
    }


    public void SetPointIndex(int index) {
        pointIndex = index;
    }


    public void ItemGet(Item item)
    {
        switch (item)
        {
            case Item.Booster:
                StartCoroutine(ItemBoosterEf());    // Booster Get
                isPointFull[pointIndex] = false;     // Item Spawn Point Reset
                break;

            case Item.Coin:
                StartCoroutine(ItemCoinEf());       // Coin Get
                isPointFull[pointIndex] = false;     // Item Spawn Point Reset
                break;

            case Item.Star:
                StartCoroutine(ItemStarEf());       // Star Get
                isPointFull[pointIndex] = false;     // Item Spawn Point Reset
                break;
        }
    }


    IEnumerator ItemBoosterEf()
    {
        UIController.instance.ItemIcon(Item.Booster, true);
        PlayerController.instance.isItemGet = true;

        itemSFX.Play();
        PlayerController.instance.speedMulti = 2;

        while(boostTime > 0)        // Timer Start
        {
            boostTime -= 1;
            yield return new WaitForSeconds(1);
        }

        boostTime = boostTimeTmp;   // Timer End

        PlayerController.instance.speedMulti = 1;

        PlayerController.instance.isItemGet = false;
        UIController.instance.ItemIcon(Item.Booster, false);
    }


    IEnumerator ItemCoinEf()
    {
        UIController.instance.ItemIcon(Item.Coin, true);
        PlayerController.instance.isItemGet = true;

        itemSFX.Play();
        PlayerController.instance.score += coinScore;

        while(coinTime > 0)        // Timer Start
        {
            coinTime -= 1;
            yield return new WaitForSeconds(1);
        }

        coinTime = coinTimeTmp;   // Timer End

        PlayerController.instance.isItemGet = false;
        UIController.instance.ItemIcon(Item.Coin, false);
    }


    IEnumerator ItemStarEf()
    {   
        UIController.instance.ItemIcon(Item.Star, true);   
        PlayerController.instance.isItemGet = true;

        itemSFX.Play();
        PlayerController.instance.isStarGet = true;
        UIController.instance.StarGauge(new Color(0.8666667f, 0.854902f, 0));

        while (starTime > 0)         // Timer Start
        {
            starTime -= 1;
            yield return new WaitForSeconds(1);
        }

        starTime = starTimeTmp;     // Time End

        PlayerController.instance.isStarGet = false;
        UIController.instance.StarGauge(new Color(0, 0.8666667f, 0.05882353f));

        PlayerController.instance.isItemGet = false;
        UIController.instance.ItemIcon(Item.Star, false);  
    }


    IEnumerator ItemRandomSpawn()
    {
        while (true)
        {
            if (itemSpawnCoolTime <= 0)     // Time OUT; Item Spawn
            {
                // Item Random Select && Spawn //
                randomPointIndex = Random.Range(0, ItemSpawnPoints.Length);   // Spawn Point Check; Is Point Empty?

                if (!isPointFull[randomPointIndex])   // If Empty, Item Spawn at Point
                {
                    isPointFull[randomPointIndex] = true;
                    itemIndex = Random.Range(0, System.Enum.GetValues(typeof(Item)).Length);   // Item Random Select
                    Instantiate(Items[itemIndex], ItemSpawnPoints[randomPointIndex].transform.position, Quaternion.identity).GetComponent<ItemIndex>().index = randomPointIndex;
                }

                itemSpawnCoolTime = itemSpawnCoolTimeTmp;   // Spawn Timer Reset
            }
            else
            {
                itemSpawnCoolTime -= 1;
                yield return new WaitForSeconds(1);
            }
        }
    }
}