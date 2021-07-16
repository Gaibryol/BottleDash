using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour
{

    public int passedLevel;
    public int highscore;

    private void Start()
    {
        passedLevel = GetPassedLevel();
        highscore = GetHighscore();
    }

    public void ChangePassedLevel(int num)
    {
        passedLevel = num;
        PlayerPrefs.SetInt("passedLevel", passedLevel);
    }

    public void ChangeHighscore(int num)
    {
        highscore = num;
        PlayerPrefs.SetInt("highscore", passedLevel);
    }

    public int GetPassedLevel()
    {
        if (PlayerPrefs.HasKey("passedLevel")) 
        {
            return PlayerPrefs.GetInt("passedLevel");
        }

        return 0;
    }

    public int GetHighscore()
    {
        if (PlayerPrefs.HasKey("highscore"))
        {
            return PlayerPrefs.GetInt("highscore");
        }
        return 0;
    }
}
