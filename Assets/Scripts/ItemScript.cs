using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{

    public bool isHeld;
    public string itemType;
    public int value;
    public bool onBelt;
    public bool inBasket;
    public GameObject basket;
    private GameObject lastBasket;

    private Vector3 pickUpPos;

    private Rigidbody2D rbody;

    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        isHeld = false;
        inBasket = false;
        onBelt = false;
    }

    // Update is called once per frame
    void Update()
    {
        ConveyorBelt();
    }

    private void OnMouseDrag()
    {
        onBelt = false;
        isHeld = true;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float posY = mousePos.y + GetComponent<SpriteRenderer>().bounds.size.y / 2.5f;
        transform.position = new Vector3(mousePos.x, posY, 0);
        MouseScript.heldItem = this.gameObject;
        GetComponent<SpriteRenderer>().sortingOrder = 20;

        if (inBasket)
        {
            MouseScript.overBasket.GetComponent<BinScript>().bottleList.Remove(this.gameObject);
            inBasket = false;
        }
    }

    private void OnMouseDown()
    {
        onBelt = false;
        pickUpPos = transform.position;
        isHeld = true;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float posY = mousePos.y + GetComponent<SpriteRenderer>().bounds.size.y / 2.5f;
        transform.position = new Vector3(mousePos.x, posY, 0);
        MouseScript.heldItem = this.gameObject;
        GetComponent<SpriteRenderer>().sortingOrder = 20;
        
        if (inBasket)
        {
            MouseScript.overBasket.GetComponent<BinScript>().bottleList.Remove(this.gameObject);
            inBasket = false;
            lastBasket = basket;
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
            GetComponent<SpriteRenderer>().sortingOrder = MouseScript.overBasket.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder - 1;
            inBasket = true;
            basket = MouseScript.overBasket;
        }
        else if (MouseScript.overBasket == null && lastBasket != null && MouseScript.overBelt == null)
        {
            transform.position = pickUpPos;
            basket = lastBasket;
            inBasket = true;
            basket.GetComponent<BinScript>().Add(this.gameObject);
            GetComponent<SpriteRenderer>().sortingOrder = MouseScript.overBasket.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder - 1;
        }
        else if (MouseScript.overBasket == null && lastBasket != null && MouseScript.overBelt != null)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
        else if (MouseScript.overBasket == null && lastBasket != null && MouseScript.overBelt == null)
        {
            transform.position = pickUpPos;
            GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
    }

    public void ConveyorBelt()
    {
        if (!isHeld && !inBasket && onBelt)
        {
            transform.Translate(new Vector2(-moveSpeed, 0));
        }
    }
}
