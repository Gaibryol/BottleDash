using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScript : MonoBehaviour
{
    public GameObject everything;
    public GameObject spawner;
    private SpawnManager sScript;
    private GameObject panel;

    public GameObject credits;
    public GameObject help;

    public List<GameObject> levelList;
    public int currentLevelPanel;

    // Start is called before the first frame update
    void Start()
    {
        sScript = spawner.GetComponent<SpawnManager>();
        panel = transform.GetChild(0).gameObject;
        everything.SetActive(false);
        sScript.playing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void NextLevelPanel()
    {
        levelList[currentLevelPanel].SetActive(false);

        if (currentLevelPanel == levelList.Count - 1)
        {
            currentLevelPanel = 0;
            levelList[currentLevelPanel].SetActive(true);
            return;
        }

        currentLevelPanel += 1;
        levelList[currentLevelPanel].SetActive(true);
    }

    public void PreviousLevelPanel()
    {
        levelList[currentLevelPanel].SetActive(false);

        if (currentLevelPanel == 0)
        {
            currentLevelPanel = levelList.Count - 1;
            levelList[currentLevelPanel].SetActive(true);
            return;
        }
        
        currentLevelPanel -= 1;
        levelList[currentLevelPanel].SetActive(true);
    }

    public void Play()
    {
        everything.SetActive(true);
        panel.SetActive(false);
        credits.SetActive(false);
        help.SetActive(false);
        levelList[currentLevelPanel].SetActive(false);
    }

    public void OpenMenu()
    {
        panel.SetActive(true);
        everything.SetActive(false);
        sScript.playing = false;
        sScript.musicManager.GetComponent<AudioSource>().Stop();
    }

    public void ToggleCredits()
    {
        if (credits.activeSelf)
        {
            credits.SetActive(false);
        }
        else
        {
            credits.SetActive(true);
            help.SetActive(false);
            levelList[currentLevelPanel].SetActive(false);
        }
    }

    public void ToggleHelp()
    {
        if (help.activeSelf)
        {
            help.SetActive(false);
        }
        else
        {
            help.SetActive(true);
            credits.SetActive(false);
            levelList[currentLevelPanel].SetActive(false);
        }
    }

    public void ToggleLevels()
    {
        if (levelList[currentLevelPanel].activeSelf)
        {
            levelList[currentLevelPanel].SetActive(false);
        }
        else
        {
            levelList[currentLevelPanel].SetActive(true);
            help.SetActive(false);
            credits.SetActive(false);
        }
    }
}
