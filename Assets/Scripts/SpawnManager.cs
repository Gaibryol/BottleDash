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
    public float variedSpawn;
    private float timer;

    public List<GameObject> levels;
    public int currentLevelNum;
    public GameObject currentLevel;

    public GameObject endgame;

    private int num;
    public float gameTimer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        variedSpawn = spawnCD;
        gameTimer = 0f;

        if (currentLevelNum != -1)
        {
            PlayLevel(currentLevelNum);
        }
        else if (currentLevelNum == -1)
        {
            PlayEndless();
        }

        num = 0;
    }

    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.deltaTime;

        if (currentLevel == null)
        {
            spawnCD = spawnCD - (gameTimer / 200000f);
            if (spawnCD <= 0.4f)
            {
                spawnCD = 0.4f;
            }

            if (timer < variedSpawn)
            {
                timer += Time.deltaTime;
            }
            else if (timer >= variedSpawn && spawning)
            {
                SpawnRandom();
                timer = 0;
                variedSpawn = spawnCD + Random.Range(-0.25f, 0.25f);
            }
        }
        else
        {
            if (timer < variedSpawn && currentLevel.GetComponent<LevelScript>().bottleList.Count > 0)
            {
                timer += Time.deltaTime;
            }
            else if (timer >= variedSpawn && currentLevel.GetComponent<LevelScript>().bottleList.Count > 0)
            {
                timer = 0;
                Spawn();
                variedSpawn = currentLevel.GetComponent<LevelScript>().spawnRate + Random.Range(-0.5f, 0.5f);
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
        GameObject bottle = currentLevel.GetComponent<LevelScript>().bottleList[Random.Range(0, currentLevel.GetComponent<LevelScript>().bottleList.Count - 1)];
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

        num += 1;
    }

    private void CheckQuota()
    {
        if (currentLevel.GetComponent<LevelScript>().bottleList.Count == 0)
        {
            timer += Time.deltaTime;
        }
        if (timer > 10f)
        {
            var binList = GameObject.FindGameObjectsWithTag("Basket");
            for (int i = 0; i < binList.Length; i++)
            {
                binList[i].GetComponent<BinScript>().Empty();
            }

            if (MoneyManager.amount < currentLevel.GetComponent<LevelScript>().quota)
            {
                // Lose
                print("Lose");
                endgame.GetComponent<EndgameScript>().Lose();
            }
            else if (MoneyManager.amount >= currentLevel.GetComponent<LevelScript>().quota)
            {
                //Win
                print("Win");
                endgame.GetComponent<EndgameScript>().Win();
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
        currentLevel.GetComponent<LevelScript>().sManager = this.gameObject;
        variedSpawn = currentLevel.GetComponent<LevelScript>().spawnRate;
    }

    public void PlayEndless()
    {
        timer = 0f;
        MoneyManager.amount = 0;
        var itemArray = GameObject.FindGameObjectsWithTag("Item");
        for (int i = 0; i < itemArray.Length; i++)
        {
            Destroy(itemArray[i].gameObject);
        }
        currentLevelNum = -1;
        currentLevel = null;
        spawnCD = 1.5f;
        variedSpawn = spawnCD;
    }

    public void Restart()
    {
        // Restart Game
        if (currentLevelNum != -1)
        {
            PlayLevel(currentLevelNum);
        }
        else if (currentLevelNum == -1)
        {
            PlayEndless();
        }
    }
}
