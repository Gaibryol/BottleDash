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
        // Line up bottles in basket
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

        bottleList.Clear();

        MoneyManager.Add(amount);
        amount = 0;
    }

    public void Clear()
    {
        for (int i = 0; i < bottleList.Count; i++)
        {
            Destroy(bottleList[i]);
        }

        bottleList.Clear();
        numItems = 0;
        amount = 0;
    }

    public void Add(GameObject item)
    {
        bottleList.Add(item);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Mouse")
        {
            MouseScript.overBasket = this.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Mouse")
        {
            MouseScript.overBasket = null;
        }
    }
}
