using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> bottles;
    public GameObject topLeft;
    public GameObject topRight;
    public GameObject bottomRight;

    public bool spawning;
    public float spawnCD;
    private float timer;

    public List<GameObject> levels;
    public int currentLevelNum;
    public GameObject currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;

        if (currentLevel != null)
        {
            currentLevelNum = levels.IndexOf(currentLevel);
            PlayLevel(currentLevelNum);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLevel == null)
        {
            if (timer < spawnCD)
            {
                timer += Time.deltaTime;
            }
            else if (timer >= spawnCD && spawning)
            {
                SpawnRandom();
                timer = 0;
                spawnCD += Random.Range(-0.5f, 0.5f);
            }
        }
        else
        {
            if (timer < currentLevel.GetComponent<LevelScript>().spawnRate && currentLevel.GetComponent<LevelScript>().bottleList.Count > 0)
            {
                timer += Time.deltaTime;
            }
            else if (timer >= currentLevel.GetComponent<LevelScript>().spawnRate && currentLevel.GetComponent<LevelScript>().bottleList.Count > 0)
            {
                timer = 0;
                spawnCD += Random.Range(-0.5f, 0.5f);
                Spawn();
            }

            CheckQuota();
        }
    }

    private GameObject GetRandomBottle()
    {
        return bottles[Mathf.RoundToInt(Random.Range(0, bottles.Count - 1))];
    }

    private GameObject GetRandomFromLevel()
    {
        GameObject bottle = currentLevel.GetComponent<LevelScript>().bottleList[Mathf.RoundToInt(Random.Range(0, currentLevel.GetComponent<LevelScript>().bottleList.Count - 1))];
        currentLevel.GetComponent<LevelScript>().bottleList.Remove(bottle);
        return bottle;

    }

    private Vector2 GetRandomPosition()
    {
        Vector2 tempPos = new Vector2(Random.Range(topLeft.transform.position.x, topRight.transform.position.x), Random.Range(topRight.transform.position.y, bottomRight.transform.position.y));
        return tempPos;
    }

    private void SpawnRandom()
    {
        Instantiate(GetRandomBottle(), GetRandomPosition(), Quaternion.identity);
    }

    private void Spawn()
    {
        Instantiate(GetRandomFromLevel(), GetRandomPosition(), Quaternion.identity);
    }

    private void CheckQuota()
    {
        if (currentLevel.GetComponent<LevelScript>().bottleList.Count == 0)
        {
            timer += Time.deltaTime;
        }
        if (timer > 10f)
        {
            if (MoneyManager.amount < currentLevel.GetComponent<LevelScript>().quota)
            {
                // Lose
                print("Lose");
            }
            else if (MoneyManager.amount >= currentLevel.GetComponent<LevelScript>().quota)
            {
                //Win
                print("Win");
            }
        }
    }

    public void PlayLevel(int num)
    {
        timer = 0f;
        MoneyManager.amount = 0;
        var itemArray = GameObject.FindGameObjectsWithTag("Item");
        for (int i = 0; i < itemArray.Length; i++)
        {
            Destroy(itemArray[i].gameObject);
        }

        currentLevelNum = num;
        currentLevel = Instantiate(levels[num]);
    }

    public void Restart()
    {
        // Restart Game
        PlayLevel(currentLevelNum);
    }
}
