using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressScript : MonoBehaviour
{
    public GameObject sManager;
    private SpawnManager sScript;
    

    // Start is called before the first frame update
    void Start()
    {
        sScript = sManager.GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Slider>().value = GetComponent<Slider>().maxValue - Mathf.RoundToInt(((float)sScript.currentLevel.GetComponent<LevelScript>().bottleList.Count / (float)sScript.currentLevel.GetComponent<LevelScript>().max) * 100);
    }
}
