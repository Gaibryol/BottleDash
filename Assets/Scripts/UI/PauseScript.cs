using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    private GameObject pauseMenu;
    public bool isPaused;

    public Sprite pauseA;
    public Sprite pauseB;
    public GameObject pauseButton;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = transform.GetChild(0).gameObject;
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else if (!isPaused)
        {
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
    }
    
    public void Pause()
    {
        isPaused = !isPaused;
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }
        else
        {
            pauseMenu.SetActive(true);
        }
    }

    public void ChangeButton()
    {
        if (pauseButton.GetComponent<Image>().sprite == pauseA)
        {
            pauseButton.GetComponent<Image>().sprite = pauseB;
        }
        else if (pauseButton.GetComponent<Image>().sprite == pauseB)
        {
            pauseButton.GetComponent<Image>().sprite = pauseA;
        }
    }
}
