using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashScript : MonoBehaviour
{
    public Sprite regular;
    public Sprite flash;
    public float delay;
    private float timer;

    public bool isFlashing;
    // Start is called before the first frame update
    void Start()
    {
        timer = delay;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlashing)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (GetComponent<SpriteRenderer>().sprite == regular)
                {
                    GetComponent<SpriteRenderer>().sprite = flash;
                    transform.GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = regular;
                    transform.GetChild(0).gameObject.SetActive(true);
                }
                timer = delay;
            }
        }
    }
}
