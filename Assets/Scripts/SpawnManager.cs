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

    public int numCollect;
    public float gameTimer;

    public GameObject musicManager;
    public GameObject effectManager;
    public AudioClip quotaReached;

    private bool done;
    public int numDrop;
    public int maxDrop;

    public bool playing;
    private bool playedQuota;

    public List<GameObject> spawned;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        variedSpawn = spawnCD;
        gameTimer = 0f;
        numDrop = 0;
        maxDrop = 10;
        playing = false;

        numCollect = 0;
        done = false;
        playedQuota = false;

        if (!playing)
        {
            effectManager.GetComponent<AudioSource>().Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            gameTimer += Time.deltaTime;

            if (currentLevel == null)
            {
                spawnCD = spawnCD - (gameTimer / 400000f);
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
                    variedSpawn = spawnCD + Random.Range(-0.2f, 0.2f);
                }

                CheckEndless();
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
        var item = Instantiate(GetRandomBottle(), GetRandomPosition(), Quaternion.identity);
        item.GetComponent<ItemScript>().effectManager = effectManager;
    }

    private void Spawn()
    {
        var item = Instantiate(GetRandomFromLevel(), GetRandomPosition(), Quaternion.identity);
        item.GetComponent<ItemScript>().effectManager = effectManager;
    }

    private void CheckQuota()
    {
        if (currentLevel.GetComponent<LevelScript>().bottleList.Count == 0 && !done)
        {
            timer += Time.deltaTime;
            musicManager.GetComponent<AudioSource>().volume -= 0.00015f;
        }
        if (MoneyManager.amount >= currentLevel.GetComponent<LevelScript>().quota && !done && !playedQuota)
        {
            effectManager.GetComponent<AudioSource>().PlayOneShot(quotaReached);
            playedQuota = true;
        }
        if (timer > 10f && !done)
        {
            if (MoneyManager.amount < currentLevel.GetComponent<LevelScript>().quota)
            {
                // Lose
                endgame.GetComponent<EndgameScript>().FadeIn(1);
                musicManager.GetComponent<AudioSource>().Stop();
                effectManager.GetComponent<AudioSource>().Stop();
                done = true;
            }
            else if (MoneyManager.amount >= currentLevel.GetComponent<LevelScript>().quota)
            {
                //Win
                endgame.GetComponent<EndgameScript>().FadeIn(0);
                musicManager.GetComponent<AudioSource>().Stop();
                effectManager.GetComponent<AudioSource>().Stop();
                done = true;
            }
        }
    }

    private void CheckEndless()
    {
        if (numDrop >= maxDrop && !done)
        {
            spawning = false;
            var items = GameObject.FindGameObjectsWithTag("Item");
            for (int i = 0; i < items.Length; i++)
            {
                Destroy(items[i].gameObject);
            }

            endgame.GetComponent<EndgameScript>().FadeIn(2);
            musicManager.GetComponent<AudioSource>().Stop();
            effectManager.GetComponent<AudioSource>().Stop();
            done = true;
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
        var binArray = GameObject.FindGameObjectsWithTag("Basket");
        for (int i = 0; i < binArray.Length; i++)
        {
            binArray[i].GetComponent<BinScript>().Clear();
        }
        currentLevelNum = num;
        currentLevel = Instantiate(levels[num]);
        currentLevel.GetComponent<LevelScript>().sManager = this.gameObject;
        variedSpawn = currentLevel.GetComponent<LevelScript>().spawnRate;

        musicManager.GetComponent<AudioSource>().Play();
        musicManager.GetComponent<AudioSource>().volume = 0.1f;
        effectManager.GetComponent<AudioSource>().Play();
        playing = true;
        numCollect = 0;
        done = false;
        spawning = true;
        playedQuota = false;
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
        var binArray = GameObject.FindGameObjectsWithTag("Basket");
        for (int i = 0; i < binArray.Length; i++)
        {
            binArray[i].GetComponent<BinScript>().Clear();
        }
        currentLevelNum = -1;
        currentLevel = null;
        spawnCD = 1.5f;
        variedSpawn = spawnCD;
        numDrop = 0;
        numCollect = 0;

        musicManager.GetComponent<AudioSource>().Play();
        musicManager.GetComponent<AudioSource>().volume = 0.1f;
        effectManager.GetComponent<AudioSource>().Play();
        playing = true;
        done = false;
        spawning = true;
        playedQuota = false;
    }

    public void Restart()
    {
        // Restart Game
        if (currentLevelNum != -1)
        {
            PlayLevel(currentLevelNum);
        }
        else
        {
            PlayEndless();
        }
    }

    public void NextLevel()
    {
        // Next Level
        if (currentLevelNum != 2)
        {
            PlayLevel(currentLevelNum + 1);
        }
    }
}
