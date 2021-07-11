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
    public int sortPos;

    public Sprite notSelected;
    public Sprite selected;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        notSelected = GetComponent<SpriteRenderer>().sprite;
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
        GetComponent<SpriteRenderer>().sortingOrder = 50;
        GetComponent<SpriteRenderer>().sprite = selected;

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
        GetComponent<SpriteRenderer>().sortingOrder = 50;
        GetComponent<SpriteRenderer>().sprite = selected;
        
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
        GetComponent<SpriteRenderer>().sprite = notSelected;
        isHeld = false;
        MouseScript.heldItem = null;

        if (MouseScript.overBasket != null && MouseScript.overBasket.GetComponent<BinScript>().open)
        {
            MouseScript.overBasket.GetComponent<BinScript>().Add(this.gameObject);
            GetComponent<SpriteRenderer>().sortingOrder = MouseScript.overBasket.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder - 10 + sortPos;
            inBasket = true;
            basket = MouseScript.overBasket;
        }
        else if (MouseScript.overBasket != null && !MouseScript.overBasket.GetComponent<BinScript>().open)
        {
            transform.position = pickUpPos;
            GetComponent<SpriteRenderer>().sortingOrder = sortPos;
        }
        else if (MouseScript.overBasket == null && lastBasket != null && MouseScript.overBelt == null)
        {
            transform.position = pickUpPos;
            basket = lastBasket;
            inBasket = true;
            basket.GetComponent<BinScript>().Add(this.gameObject);
            GetComponent<SpriteRenderer>().sortingOrder = basket.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder - 10 + sortPos;
        }
        else if (MouseScript.overBasket == null && lastBasket != null && MouseScript.overBelt != null)
        {
            GetComponent<SpriteRenderer>().sortingOrder = sortPos;
        }
        else if (MouseScript.overBasket == null && lastBasket == null && MouseScript.overBelt == null)
        {
            transform.position = pickUpPos;
            GetComponent<SpriteRenderer>().sortingOrder = sortPos;
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
