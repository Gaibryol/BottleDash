using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public int[] bottles;
    public int quota;
    public float spawnRate;
    public List<GameObject> bottleList;
    public GameObject sManager;

    // Start is called before the first frame update
    void Start()
    {
        MakeList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MakeList()
    {
        for (int i = 0; i < bottles.Length; i++)
        {
            for (int j = 0; j < bottles[i]; j++)
            {
                bottleList.Add(sManager.GetComponent<SpawnManager>().bottles[i]);
            }
        }
    }
}
