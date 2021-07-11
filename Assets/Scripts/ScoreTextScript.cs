using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreTextScript : MonoBehaviour
{
    public GameObject sManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = MoneyManager.amount.ToString() + " / " + sManager.GetComponent<SpawnManager>().currentLevel.GetComponent<LevelScript>().quota;
    }
}
