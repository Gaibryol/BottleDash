using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public int[] numBottles;
    public int quota;
    public float spawnRate;
    public List<GameObject> bottleList;
    public GameObject sManager;
    public int max;

    // Start is called before the first frame update
    void Start()
    {
        MakeList();
        max = bottleList.Count;
        sManager = GameObject.FindGameObjectWithTag("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        print("num: " + bottleList.Count);
    }

    private void MakeList()
    {
        for (int i = 0; i < numBottles.Length; i++)
        {
            for (int j = 0; j < numBottles[i]; j++)
            {
                bottleList.Add(sManager.GetComponent<SpawnManager>().bottles[i]);
            }
        }
    }
}
