using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class GameState : MonoBehaviour
{
    EventCore eventCore;
    public TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.enabled = false;
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.winGameEV.AddListener(winGame);
        eventCore.loseGameEV.AddListener(loseGame);
    }

    void winGame()
    {
        StartCoroutine(EndGmae(true));
    }
    void loseGame()
    {
        StartCoroutine(EndGmae(false));
    }
    IEnumerator EndGmae(bool wingame)
    {
        text.enabled = true;
        if (wingame)
        {
            SetText("You Win");
        }
        else if (!wingame)
        {
            SetText("You Lose");
        }
        yield return new WaitForSeconds(1.5f);
        Application.Quit();
    }
    void SetText(string TstateText)
    {
        text.text = TstateText;
    }
}
