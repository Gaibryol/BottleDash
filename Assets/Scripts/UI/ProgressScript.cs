using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressScript : MonoBehaviour
{
    public GameObject sManager;
    private SpawnManager sScript;

    public GameObject fill;
    private Color col;
    // Start is called before the first frame update
    void Start()
    {
        sScript = sManager.GetComponent<SpawnManager>();
        col = new Color(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (sScript.currentLevelNum == -1)
        {
            // Change to lives
            fill.GetComponent<Image>().color = new Color(1, 0.3f, 0.3f);
            GetComponent<Slider>().value = Mathf.RoundToInt(((float)sScript.numDrop / (float)sScript.maxDrop) * 100);
        }
        else if(sScript.currentLevelNum != -1)
        {
            // Show Progress
            fill.GetComponent<Image>().color = col;
            GetComponent<Slider>().value = GetComponent<Slider>().maxValue - Mathf.RoundToInt(((float)sScript.currentLevel.GetComponent<LevelScript>().bottleList.Count / (float)sScript.currentLevel.GetComponent<LevelScript>().max) * 100);
        }
    }
}
