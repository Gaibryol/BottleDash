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

    // Start is called before the first frame update
    void Start()
    {
        amount = 0;
        open = true;
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
        }
        else
        {
            open = true;
        }
    }

    public void Empty()
    {
        for (int i = 0; i < bottleList.Count; i++)
        {
            if (bottleList[i].GetComponent<ItemScript>().itemType == binType)
            {
                amount += bottleList[i].GetComponent<ItemScript>().value;
            }
            Destroy(bottleList[i]);
        }

        bottleList.Clear();

        MoneyManager.Add(amount);
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
