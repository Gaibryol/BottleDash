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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnCD)
        {
            timer += Time.deltaTime;
        }
        else if (timer >= spawnCD && spawning)
        {
            SpawnRandom();
            timer = 0;
        }
    }

    private GameObject GetRandomBottle()
    {
        return bottles[Mathf.RoundToInt(Random.Range(0, bottles.Count - 1))];
    }

    private Vector2 GetRandomPosition()
    {
        return new Vector2(Random.Range(topLeft.transform.position.x, topRight.transform.position.x), Random.Range(topRight.transform.position.y, bottomRight.transform.position.y));
    }

    private void SpawnRandom()
    {
        Instantiate(GetRandomBottle(), GetRandomPosition(), Quaternion.identity);
    }
}
