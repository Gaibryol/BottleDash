using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreTextScript : MonoBehaviour
{
    public GameObject saveManager;
    private SaveScript sScript;

    public GameObject highScoreText;
    public GameObject bottleNumText;
    public GameObject dateText;

    // Start is called before the first frame update
    void Start()
    {
        sScript = saveManager.GetComponent<SaveScript>();
    }

    // Update is called once per frame
    void Update()
    {
        highScoreText.GetComponent<TextMeshProUGUI>().text = sScript.GetHighscore().ToString();
        bottleNumText.GetComponent<TextMeshProUGUI>().text = sScript.GetHighbottle().ToString();
        dateText.GetComponent<TextMeshProUGUI>().text = sScript.GetDate();
    }
}
