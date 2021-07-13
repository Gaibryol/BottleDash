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

    private Vector3 pickUpPos;

    private Rigidbody2D rbody;

    public float moveSpeed;
    public int sortPos;

    public Sprite notSelected;
    public Sprite selected;

    public bool dying;

    private Vector3 spawnPos;
    private bool spawning;

    public GameObject effectManager;
    public AudioClip pickUp;
    public AudioClip putDown;
    public AudioClip trash;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        notSelected = GetComponent<SpriteRenderer>().sprite;
        isHeld = false;
        inBasket = false;
        onBelt = false;
        dying = false;

        spawnPos = transform.position;
        transform.position = new Vector2(spawnPos.x, spawnPos.y + 2);
        GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 0);
        spawning = true;
    }

    // Update is called once per frame
    void Update()
    {
        ConveyorBelt();

        if (spawning)
        {
            isHeld = false;
            inBasket = false;
            onBelt = false;
            transform.Translate(new Vector2(0, -moveSpeed * Time.deltaTime));
            GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, GetComponent<SpriteRenderer>().color.a + Time.deltaTime);

            if (transform.position.y <= spawnPos.y)
            {
                spawning = false;
            }
        }

        if (dying)
        {
            transform.Rotate(0, 0, 25 * Time.deltaTime);
            GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, GetComponent<SpriteRenderer>().color.a - Time.deltaTime);
            transform.Translate(new Vector2(-moveSpeed * Time.deltaTime, -moveSpeed / 2f * Time.deltaTime));
        }

        if (inBasket)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnMouseDrag()
    {
        if (!dying && !spawning && !PauseScript.isPaused)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float posY = mousePos.y + GetComponent<SpriteRenderer>().bounds.size.y / 2.5f;
            transform.position = new Vector3(mousePos.x, posY, 0);

            GetComponent<SpriteRenderer>().sortingOrder = 50;
            GetComponent<SpriteRenderer>().sprite = selected;
        }
    }

    private void OnMouseDown()
    {
        if (!dying && !spawning && !PauseScript.isPaused)
        {
            effectManager.GetComponent<AudioSource>().PlayOneShot(pickUp);
            onBelt = false;
            pickUpPos = transform.position;
            isHeld = true;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float posY = mousePos.y + GetComponent<SpriteRenderer>().bounds.size.y / 2.5f;
            transform.position = new Vector3(mousePos.x, posY, 0);
            MouseScript.heldItem = this.gameObject;
            GetComponent<SpriteRenderer>().sortingOrder = 50;
            GetComponent<SpriteRenderer>().sprite = selected;
        }
    }

    private void OnMouseUp()
    {
        if (!dying && !spawning && !PauseScript.isPaused)
        {
            GetComponent<SpriteRenderer>().sprite = notSelected;
            isHeld = false;
            MouseScript.heldItem = null;

            if (MouseScript.overBasket != null && MouseScript.overBasket.GetComponent<BinScript>().open)
            {
                effectManager.GetComponent<AudioSource>().PlayOneShot(putDown);
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
            else if (MouseScript.overBasket == null && MouseScript.overBelt == null)
            {
                transform.position = pickUpPos;
                GetComponent<SpriteRenderer>().sortingOrder = sortPos;
            }
        }
    }

    public void ConveyorBelt()
    {
        if (!isHeld && !inBasket && onBelt && !dying && !spawning)
        {
            transform.Translate(new Vector2(-moveSpeed  * Time.deltaTime, 0));
        }
    }

    public void Kill()
    {
        isHeld = false;
        inBasket = false;
        onBelt = false;
        dying = true;
        Invoke("Die", 3f);
        effectManager.GetComponent<AudioSource>().PlayOneShot(trash, 0.25f);
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
