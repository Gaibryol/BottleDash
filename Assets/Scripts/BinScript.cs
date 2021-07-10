using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinScript : MonoBehaviour
{
    public string binType;

    public int maxItems;
    public int numItems;

    public Sprite empty;
    public Sprite quarter;
    public Sprite half;
    public Sprite threeQuarters;
    public Sprite full;

    public bool open;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SpriteLogic();
        FullnessLogic();
    }

    void SpriteLogic()
    {
        if (Mathf.RoundToInt((numItems / maxItems) * 100) < 25)
        {
            GetComponent<SpriteRenderer>().sprite = empty;
        }
        else if (Mathf.RoundToInt((numItems / maxItems) * 100) < 50 && Mathf.RoundToInt((numItems / maxItems) * 100) >= 25)
        {
            GetComponent<SpriteRenderer>().sprite = quarter;
        }
        else if (Mathf.RoundToInt((numItems / maxItems) * 100) < 75 && Mathf.RoundToInt((numItems / maxItems) * 100) >= 50)
        {
            GetComponent<SpriteRenderer>().sprite = half;
        }
        else if (Mathf.RoundToInt((numItems / maxItems) * 100) < 100 && Mathf.RoundToInt((numItems / maxItems) * 100) >= 75)
        {
            GetComponent<SpriteRenderer>().sprite = threeQuarters;
        }
        else if (Mathf.RoundToInt((numItems / maxItems) * 100) == 100)
        {
            GetComponent<SpriteRenderer>().sprite = full;
        }
    }

    void FullnessLogic()
    {
        if (Mathf.RoundToInt((numItems / maxItems) * 100) >= 100)
        {
            open = false;
        }
        else
        {
            open = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == binType)
        {
            ItemScript iScript = collision.gameObject.GetComponent<ItemScript>();
            if (!iScript.isHeld)
            {
                numItems += 1;
                iScript.Drop();
            }
        }
    }
}
