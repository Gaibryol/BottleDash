using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndgameScript : MonoBehaviour
{
    private GameObject endgame;

    // Start is called before the first frame update
    void Start()
    {
        endgame = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
