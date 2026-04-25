using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    EventCore eventCore;
    public GameObject endScreen;
    Image endScreenBg;

    [Header("Parameters")]

    public float secondsToAppear = 2;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        endScreen.SetActive(false);
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.winGameEV.AddListener(WinGame);
        eventCore.loseGameEV.AddListener(LoseGame);

        endScreenBg = endScreen.transform.GetChild(0).GetComponent<Image>();
    }

    void WinGame()
    {
        endScreenBg.color = new Color32(175, 255, 148, 255);

        SetText("You Win!");
        StartCoroutine(EndGame(true));
    }
    void LoseGame()
    {
        endScreenBg.color = new Color32(175, 0, 19, 255);

        SetText("You Lose...");
        StartCoroutine(EndGame(false));
    }
    IEnumerator EndGame(bool wingame)
    {
        RectTransform endScreenTransform = endScreen.GetComponent<RectTransform>();
        Vector3 originalPos = endScreenTransform.anchoredPosition;

        endScreen.SetActive(true);
        
        while (endScreenTransform.anchoredPosition.y > 0)
        {
            Vector3 newPos = endScreenTransform.anchoredPosition;
            newPos.y -= originalPos.y / secondsToAppear * Time.deltaTime;
            endScreenTransform.anchoredPosition = newPos;

            yield return new WaitForEndOfFrame();
        }

        endScreenTransform.anchoredPosition = Vector3.zero;

        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("TitleScreen");
    }
    void SetText(string TstateText)
    {
        TextMeshProUGUI endScreenText = endScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        endScreenText.text = TstateText;
    }
}
