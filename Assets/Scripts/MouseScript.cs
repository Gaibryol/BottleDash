using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    public static GameObject heldItem;
    public static GameObject overBasket;
    public static GameObject overBelt;

    private float doubleTouchTimer;
    private bool firstTouch;

    // Start is called before the first frame update
    void Start()
    {
        firstTouch = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        BasketCheck();
        //Debug.Log("Held: " + heldItem + " |  Basket: " + overBasket + " | Belt:  " + overBelt);
    }

    private void BasketCheck()
    {
        if (doubleTouchTimer > 0)
        {
            doubleTouchTimer -= Time.deltaTime;
        }
        else if (doubleTouchTimer < 0)
        {
            doubleTouchTimer = 0;
            firstTouch = true;
        }

        if (overBasket != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (firstTouch)
                {
                    firstTouch = false;
                    doubleTouchTimer = 0.3f;
                    return;
                }
                if (!firstTouch && doubleTouchTimer > 0)
                {
                    overBasket.GetComponent<BinScript>().Empty();
                    firstTouch = true;
                    return;
                }
            }
        }
    }
}
