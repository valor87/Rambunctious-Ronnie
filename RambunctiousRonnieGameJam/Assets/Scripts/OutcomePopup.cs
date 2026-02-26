using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OutcomePopup : MonoBehaviour
{
    [Header("References")]
    public Image panel;
    public TextMeshProUGUI outcomeText;

    [Header("Parameters")]
    public float fadeOutTime = 3f;

    EventCore eventCore;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.successfulShowEV.AddListener(SuccessfulPopup);
        eventCore.failureShowEV.AddListener(FailurePopup);

        if (panel == null)
        {
            panel = GetComponent<Image>();
            if (panel == null)
            {
                Debug.LogError("Could not find the panel which is supposed to be embedded in this object.");
            }
        }

        if (outcomeText == null)
        {
            outcomeText = transform.Find("OutcomeText").GetComponent<TextMeshProUGUI>();
            if (outcomeText == null)
            {
                Debug.LogError("Could not find the outcome text which is supposed to be a child.");
            }
        }
    }

    void SuccessfulPopup()
    {
        Color color = panel.color;
        color.a = 1;
        panel.color = color;

        outcomeText.text = "Success";
        color = outcomeText.color;
        color = new Color(0, 255, 0);
        color.a = 1;
        outcomeText.color = color;

        StartCoroutine(FadeOutPopup());
    }

    void FailurePopup()
    {
        Color color = panel.color;
        color.a = 1;
        panel.color = color;

        outcomeText.text = "Failure";
        color = outcomeText.color;
        color = new Color(255, 0, 0);
        color.a = 1;
        outcomeText.color = color;

        StartCoroutine(FadeOutPopup());
    }

    IEnumerator FadeOutPopup()
    {
        float timer = 0;
        while (timer < fadeOutTime)
        {
            timer += Time.deltaTime;
            
            Color color = panel.color;
            color.a -= Time.deltaTime / fadeOutTime;
            panel.color = color;

            color = outcomeText.color;
            color.a -= Time.deltaTime / fadeOutTime;
            outcomeText.color = color;

            yield return Time.deltaTime;
        }

        yield return null;
    }
}
