using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconScript : MonoBehaviour
{
    public GameObject spawner;
    private SpawnManager sScript;

    public Sprite flag;
    public Sprite skull;

    // Start is called before the first frame update
    void Start()
    {
        sScript = spawner.GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sScript.currentLevelNum == -1)
        {
            GetComponent<Image>().sprite = skull;
        }
        else
        {
            GetComponent<Image>().sprite = flag;
        }
    }
}
