using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GarbageScript : MonoBehaviour
{
    public GameObject spawner;
    private SpawnManager sScript;

    // Start is called before the first frame update
    void Start()
    {
        sScript = spawner.GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            collision.gameObject.GetComponent<ItemScript>().Kill();
            sScript.numDrop += 1;
        }
    }
}
