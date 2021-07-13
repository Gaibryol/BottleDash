using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndgameScript : MonoBehaviour
{
    private GameObject endgame;
    private bool fading;

    public GameObject effectManager;
    public AudioClip loseClip;
    public AudioClip winClip;
    public AudioClip endlessClip;

    public GameObject panel;
    public GameObject endlessPanel;

    public GameObject level;
    public GameObject coins;
    public GameObject numCollect;

    public GameObject eLevel;
    public GameObject eCoins;
    public GameObject eNumCollect;

    public GameObject spawner;
    private SpawnManager sScript;

    // Start is called before the first frame update
    void Start()
    {
        sScript = spawner.GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fading)
        {
            endgame.GetComponent<Image>().color = new Color(endgame.GetComponent<Image>().color.r, endgame.GetComponent<Image>().color.g, endgame.GetComponent<Image>().color.b, endgame.GetComponent<Image>().color.a + Time.deltaTime);

            if (endgame.GetComponent<Image>().color.a >= 1)
            {
                fading = false;
            }
        }
    }

    public void CloseUI()
    {
        panel.SetActive(false);
        endlessPanel.SetActive(false);
    }

    public void Lose()
    {
        effectManager.GetComponent<AudioSource>().PlayOneShot(loseClip, 0.25f);
    }

    public void Win()
    {
        effectManager.GetComponent<AudioSource>().PlayOneShot(winClip, 0.25f);
    }

    public void Endless()
    {
        effectManager.GetComponent<AudioSource>().PlayOneShot(endlessClip, 0.25f);
    }

    public void FadeIn(int state)
    {
        if (state == 0)
        {
            Invoke("Win", 0.5f);
            endgame = panel;

            level.GetComponent<TextMeshProUGUI>().text = "Level " + (sScript.currentLevelNum + 1).ToString();
            coins.GetComponent<TextMeshProUGUI>().text = MoneyManager.amount.ToString();
            numCollect.GetComponent<TextMeshProUGUI>().text = sScript.numCollect.ToString();
        }
        else if (state == 1)
        {
            Invoke("Lose", 0.5f);
            endgame = panel;

            level.GetComponent<TextMeshProUGUI>().text = "Level " + (sScript.currentLevelNum + 1).ToString();
            coins.GetComponent<TextMeshProUGUI>().text = MoneyManager.amount.ToString();
            numCollect.GetComponent<TextMeshProUGUI>().text = sScript.numCollect.ToString();
        }
        else if (state == 2)
        {
            // Endless
            eLevel.GetComponent<TextMeshProUGUI>().text = "Free Play";
            eCoins.GetComponent<TextMeshProUGUI>().text = MoneyManager.amount.ToString();
            eNumCollect.GetComponent<TextMeshProUGUI>().text = sScript.numCollect.ToString();

            Invoke("Endless", 0.5f);
            endgame = endlessPanel;
        }
        fading = true;
        endgame.SetActive(true);
        endgame.GetComponent<Image>().color = new Color(endgame.GetComponent<Image>().color.r, endgame.GetComponent<Image>().color.g, endgame.GetComponent<Image>().color.b, 0);
    }
}
