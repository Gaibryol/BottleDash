using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HSPopupScript : MonoBehaviour
{
    public GameObject coinText;
    public GameObject bottleText;

    public GameObject popup;
    private bool move;
    private bool enter;
    private bool exit;

    // Start is called before the first frame update
    void Start()
    {
        popup = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ExitAnim();
        }

        if (move)
        {
            popup.GetComponent<RectTransform>().Translate(Vector2.up * Time.deltaTime * 2000);

            if (enter)
            {
                if (popup.GetComponent<RectTransform>().anchoredPosition.y >= 0)
                {
                    move = false;
                    enter = false;
                }
            }

            if (exit)
            {
                if (popup.GetComponent<RectTransform>().anchoredPosition.y >= Screen.height)
                {
                    move = false;
                    exit = false;
                    gameObject.SetActive(false);
                }
            }
        }
    }

    public void ChangeText(int numCoin, int numBottle)
    {
        coinText.GetComponent<TextMeshProUGUI>().text = numCoin.ToString();
        bottleText.GetComponent<TextMeshProUGUI>().text = numBottle.ToString();
    }

    public void EnterPopUp()
    {
        move = true;
        enter = true;
        popup.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (-Screen.height / 2) - 100);
    }

    public void ExitPopUp()
    {
        move = true;
        exit = true;
    }

    public void FadeToBlack()
    {
        GetComponent<CanvasRenderer>().SetAlpha(0);
        GetComponent<Image>().CrossFadeAlpha(1f, 0.5f, false);
    }

    public void FadeOut()
    {
        GetComponent<Image>().CrossFadeAlpha(0f, 0.5f, false);
        Invoke("SetInactive", 0.5f);
    }

    public void SetInactive()
    {
        gameObject.SetActive(false);
    }

    public void EnterAnim()
    {
        FadeToBlack();
        EnterPopUp();
    }

    public void ExitAnim()
    {
        FadeOut();
        ExitPopUp();
    }
}
