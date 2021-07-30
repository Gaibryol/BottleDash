using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinScript : MonoBehaviour
{
    public string binType;

    public int maxItems;
    public int numItems;

    public bool open;
    public int amount;

    public List<GameObject> bottleList;

    public GameObject effectManager;
    public GameObject spawner;
    private SpawnManager sScript;
    public AudioClip swap;

    // Start is called before the first frame update
    void Start()
    {
        amount = 0;
        open = true;
        sScript = spawner.GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        SpriteLogic();
        FullnessLogic();
    }

    void SpriteLogic()
    {
        if (bottleList.Count != 0)
        {
            // Line up bottles in basket
            for (int i = 0; i < bottleList.Count; i++)
            {
                bottleList[i].GetComponent<SpriteRenderer>().sortingOrder = transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder - 5 + i;
            }
        }
    }

    void FullnessLogic()
    {
        numItems = bottleList.Count;

        if (Mathf.RoundToInt((numItems / maxItems) * 100) >= 100)
        {
            open = false;
            GetComponent<FlashScript>().isFlashing = true;
        }
        else
        {
            open = true;
            GetComponent<FlashScript>().isFlashing = false;
        }
    }

    public void Empty()
    {
        effectManager.GetComponent<AudioSource>().PlayOneShot(swap);
        for (int i = 0; i < bottleList.Count; i++)
        {
            if (bottleList[i].GetComponent<ItemScript>().itemType == binType)
            {
                amount += bottleList[i].GetComponent<ItemScript>().value;
                sScript.numCollect += 1;
            }
            Destroy(bottleList[i]);
        }
        MoneyManager.Add(amount);
        numItems = 0;
        amount = 0;
        bottleList = new List<GameObject>();
    }

    public void Clear()
    {
        for (int i = 0; i < bottleList.Count; i++)
        {
            Destroy(bottleList[i]);
        }

        bottleList = new List<GameObject>();
        numItems = 0;
        amount = 0;
    }

    public void Add(GameObject item)
    {
        //bottleList.Add(item);
        if (bottleList.Count == 0)
        {
            bottleList.Add(item);
            return;
        }

        for (int i = 0; i < bottleList.Count; i++)
        {
            if (item.transform.position.y - item.GetComponent<SpriteRenderer>().bounds.size.y / 2 > bottleList[i].transform.position.y - bottleList[i].GetComponent<SpriteRenderer>().bounds.size.y / 2)
            {
                bottleList.Insert(i, item);
                return;
            }
        }
        bottleList.Add(item);
        return;
    }
}
