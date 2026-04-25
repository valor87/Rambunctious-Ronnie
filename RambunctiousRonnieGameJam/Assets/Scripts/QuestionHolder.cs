using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestionHolder : MonoBehaviour, IPointerEnterHandler
{
    EventCore eventCore;
    public TextMeshProUGUI textObj;
    public Question question;
    public bool questionDisabled;
    public AudioClip hoverSfx;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();

        eventCore.approveCharacterEV.AddListener(DisableQuestion);
        eventCore.denyCharacterEV.AddListener(DisableQuestion);
        eventCore.reachedMaxQuestionsEV.AddListener(DisableQuestion);

        eventCore.createNewCharacterEV.AddListener(EnableQuestion);
        eventCore.followApproveCharacterEV.AddListener(EnableQuestion);
        eventCore.winGameEV.AddListener(DisableSelf);
        eventCore.loseGameEV.AddListener(DisableSelf);

        if (question.questionText == null)
        {
            Debug.LogWarning("Question is missing its description text. Without this, player can't tell what the question is supposed to be.");
        }

        textObj.text = question.questionText;
    }

    public void invokeAskQuestion()
    {
        if (!questionDisabled)
        {
            eventCore.askQuestionEV.Invoke(question);
            //eventCore.playOneShotEV.Invoke(clickSfx);
            DisableQuestion();
        }
    }

    void DisableQuestion()
    {
        questionDisabled = true;
        textObj.color = new Color(0.75f, 0.75f, 0.75f);
    }
    
    void EnableQuestion()
    {
        questionDisabled = false;
        textObj.color = Color.black;
    }

    void DisableSelf()
    {
        gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!questionDisabled)
        {
            eventCore.playOneShotEV.Invoke(hoverSfx);
        }
        
    }
}
