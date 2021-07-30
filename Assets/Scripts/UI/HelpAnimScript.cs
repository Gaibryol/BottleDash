using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpAnimScript : MonoBehaviour
{
    private Animator anim;
    private const string FLASH = "Flash";
    private const string SWIPE = "Swipe";

    private float timer;
    public float pace;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= pace)
        {
            if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == FLASH)
            {
                anim.Play(SWIPE);
                timer = 0f;
            }
            else if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == SWIPE)
            {
                anim.Play(FLASH);
                timer = 0f;
            }
        }
    }
}
