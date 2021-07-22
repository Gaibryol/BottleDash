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

    private bool fingerDown;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        firstTouch = true;
        fingerDown = false;
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
            //if (Input.GetMouseButtonDown(0))
            //{
            //    if (firstTouch)
            //    {
            //        firstTouch = false;
            //        doubleTouchTimer = 0.3f;
            //        return;
            //    }
            //    if (!firstTouch && doubleTouchTimer > 0)
            //    {
            //        overBasket.GetComponent<BinScript>().Empty();
            //        firstTouch = true;
            //        return;
            //    }
            //}

            if ((Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer))
            {
                if (fingerDown == false && Input.GetMouseButtonDown(0))
                {
                    startPos = Input.mousePosition;
                    fingerDown = true;
                }

                if (fingerDown)
                {
                    if (Input.mousePosition.x >= startPos.x + 30)
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

            if (Application.platform == RuntimePlatform.Android)
            {
                if (fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
                {
                    fingerDown = true;
                    startPos = Input.touches[0].position;
                }

                if (fingerDown)
                {
                    if (Input.touches[0].position.x >= startPos.x + 30)
                    {
                        fingerDown = false;
                        overBasket.GetComponent<BinScript>().Empty();
                        return;
                    }
                }

                if (fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
                {
                    fingerDown = false;
                }
            }
        }
    }
}
