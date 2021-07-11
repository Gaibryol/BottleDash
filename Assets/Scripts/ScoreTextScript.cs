using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreTextScript : MonoBehaviour
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
        if (sScript.currentLevelNum == -1)
        {
            // Endless Mode
            GetComponent<TextMeshProUGUI>().text = MoneyManager.amount.ToString();
        }
        else
        {
            // Show Money and Quota
            GetComponent<TextMeshProUGUI>().text = MoneyManager.amount.ToString() + " / " + sScript.currentLevel.GetComponent<LevelScript>().quota;
        }
    }
}
