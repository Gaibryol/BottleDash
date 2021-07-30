using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    public static GameObject heldItem;
    public static GameObject overBasket;
    public static GameObject overBelt;

    public bool fingerDown;
    public Vector3 startPos;

    public GameObject downBasket;
    private Vector2 downPos;
    private Vector2 upPos;
    private Touch touch;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MouseCheck();
        BasketCheck();
    }

    private void MouseCheck()
    {
        if ((Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D ray = Physics2D.Raycast(mousePos, Vector2.zero, 1.0f, ~(LayerMask.GetMask("Items")));

            if (ray.collider != null)
            {
                if (ray.collider.gameObject.tag == "Basket")
                {
                    overBasket = ray.collider.gameObject;
                    overBelt = null;
                }
                else if (ray.collider.gameObject.tag == "Belt")
                {
                    overBasket = null;
                    overBelt = ray.collider.gameObject;
                }
            }
            else
            {
                overBasket = null;
                overBelt = null;
            }
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

                RaycastHit2D ray = Physics2D.Raycast(mousePos, Vector2.zero, 1.0f, ~(LayerMask.GetMask("Items")));

                if (ray.collider != null)
                {
                    if (ray.collider.gameObject.tag == "Basket")
                    {
                        overBasket = ray.collider.gameObject;
                        overBelt = null;
                    }
                    else if (ray.collider.gameObject.tag == "Belt")
                    {
                        overBasket = null;
                        overBelt = ray.collider.gameObject;
                    }
                }
                else
                {
                    overBasket = null;
                    overBelt = null;
                }
            }
        }
    }

    private void BasketCheck()
    {
        if ((Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer))
        {
            if (overBasket != null && heldItem == null)
            {
                if (fingerDown == false && Input.GetMouseButtonDown(0))
                {
                    startPos = Input.mousePosition;
                    fingerDown = true;
                }

                if (fingerDown)
                {
                    if (Input.mousePosition.x >= startPos.x + 60 && Mathf.Abs(Input.mousePosition.x - startPos.x) > Mathf.Abs(Input.mousePosition.y - startPos.y))
                    {
                        // Swipe Right
                        fingerDown = false;
                        overBasket.GetComponent<BinScript>().Empty();
                        return;
                    }
                }

                if (fingerDown && Input.GetMouseButtonUp(0))
                {
                    fingerDown = false;
                }
            }
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            if (overBasket != null && heldItem == null)
            {
                if (fingerDown == false && Input.touchCount > 0)
                {
                    startPos = Input.mousePosition;
                    fingerDown = true;
                }

                if (fingerDown)
                {
                    if (Input.mousePosition.x >= startPos.x + 60 && Mathf.Abs(Input.mousePosition.x - startPos.x) > Mathf.Abs(Input.mousePosition.y - startPos.y))
                    {
                        // Swipe Right
                        fingerDown = false;
                        overBasket.GetComponent<BinScript>().Empty();
                        return;
                    }
                }

                if (fingerDown && Input.GetMouseButtonUp(0))
                {
                    fingerDown = false;
                }
            }
        }
    }
}
