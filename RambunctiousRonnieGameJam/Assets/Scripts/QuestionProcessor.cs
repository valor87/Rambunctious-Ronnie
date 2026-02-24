using UnityEngine;

public class QuestionProcessor : MonoBehaviour
{
    EventCore eventCore;
    public GameObject characterObj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.askQuestionEV.AddListener(ProcessQuestion);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ProcessQuestion(Question question)
    {
        bool didUniqueReply = false; //temporary, will remove later
        Character characterData = characterObj.GetComponent<CharacterValues>().CharactersValues;
        for (int i = 0; i < question.traitsRevealed.Count; i++)
        {
            Trait selectedTrait = question.traitsRevealed[i];
            if (characterData.traitList.Contains(selectedTrait))
            {
                print($"Revealed Trait: {selectedTrait}");
            }
        }
        
        for (int i = 0; i < question.replyKey.Count; i++)
        {
            Trait selectedTrait = question.replyKey[i];
            if (characterData.traitList.Contains(selectedTrait))
            {
                print($"Trait: {selectedTrait} \nReply: {question.replyText[i]}");
                didUniqueReply = true;
            }
        }

        if (!didUniqueReply)
        {
            print($"Default reply: {question.defaultReply}");
        }
    }
}
