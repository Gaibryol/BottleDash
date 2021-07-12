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
    public GameObject levels;

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

    public void Play()
    {
        everything.SetActive(true);
        panel.SetActive(false);
        credits.SetActive(false);
        help.SetActive(false);
        levels.SetActive(false);
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
            levels.SetActive(false);
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
            levels.SetActive(false);
        }
    }

    public void ToggleLevels()
    {
        if (levels.activeSelf)
        {
            levels.SetActive(false);
        }
        else
        {
            levels.SetActive(true);
            help.SetActive(false);
            credits.SetActive(false);
        }
    }
}
