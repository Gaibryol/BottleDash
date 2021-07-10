using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{

    public bool isHeld;
    public string itemType;
    public int value;

    public bool inBasket;
    public GameObject basket;

    // Start is called before the first frame update
    void Start()
    {
        isHeld = false;
        inBasket = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnMouseDrag()
    {
        isHeld = true;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        MouseScript.heldItem = this.gameObject;
        GetComponent<SpriteRenderer>().sortingOrder = 5;

        if (inBasket)
        {
            MouseScript.overBasket.GetComponent<BinScript>().bottleList.Remove(this.gameObject);
            inBasket = false;
        }
    }

    private void OnMouseDown()
    {
        isHeld = true;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        MouseScript.heldItem = this.gameObject;
        GetComponent<SpriteRenderer>().sortingOrder = 5;

        if (inBasket)
        {
            MouseScript.overBasket.GetComponent<BinScript>().bottleList.Remove(this.gameObject);
            inBasket = false;
            basket = null;
        }
    }

    private void OnMouseUp()
    {
        isHeld = false;
        MouseScript.heldItem = null;

        if (MouseScript.overBasket != null)
        {
            MouseScript.overBasket.GetComponent<BinScript>().Add(this.gameObject);
            GetComponent<SpriteRenderer>().sortingOrder = 1;
            inBasket = true;
            basket = MouseScript.overBasket;
        }
    }
}
