using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour
{

    public int passedLevel;
    public int highscore;
    public int highbottle;
    public string date;

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
        PlayerPrefs.SetInt("highscore", highscore);
    }

    public void ChangeHighbottle(int num)
    {
        highbottle = num;
        PlayerPrefs.SetInt("highbottle", highbottle);
    }

    public void ChangeDate(string d)
    {
        date = d;
        PlayerPrefs.SetString("date", date);
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

    public int GetHighbottle()
    {
        if (PlayerPrefs.HasKey("highbottle"))
        {
            return PlayerPrefs.GetInt("highbottle");
        }

        return 0;
    }

    public string GetDate()
    {
        if (PlayerPrefs.HasKey("date"))
        {
            return PlayerPrefs.GetString("date");
        }

        return "";
    }
}
