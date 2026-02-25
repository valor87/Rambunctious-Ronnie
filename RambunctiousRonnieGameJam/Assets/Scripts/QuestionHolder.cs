using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionHolder : MonoBehaviour
{
    EventCore eventCore;
    public TextMeshProUGUI textObj;
    public Question question;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.winGameEV.AddListener(disableSelf);
        eventCore.loseGameEV.AddListener(disableSelf);

        if (question.questionText == null)
        {
            Debug.LogWarning("Question is missing its description text. Without this, player can't tell what the question is supposed to be.");
        }

        textObj.text = question.questionText;
    }

    public void invokeAskQuestion()
    {
        print("invoking askQuestionEV");
        eventCore.askQuestionEV.Invoke(question);
    }

    void disableSelf()
    {
        gameObject.SetActive(false);
    }
}
