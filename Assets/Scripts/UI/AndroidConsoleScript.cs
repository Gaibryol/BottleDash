using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AndroidConsoleScript : MonoBehaviour
{
    public GameObject mouse;
    private MouseScript mScript;

    // Start is called before the first frame update
    void Start()
    {
        mScript = mouse.GetComponent<MouseScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MouseScript.overBasket != null)
        {
            GetComponent<TextMeshProUGUI>().text = MouseScript.overBasket.ToString();
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = "Null";
        }
    }
}
