using TMPro;
using UnityEngine;

public class QuestionProcessor : MonoBehaviour
{
    EventCore eventCore;
    public GameObject characterObj;
    public TextMeshProUGUI textbox;
    public TextMeshProUGUI[] traitTexts = new TextMeshProUGUI[3];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.askQuestionEV.AddListener(ProcessQuestion);
        eventCore.setNewCharacterEV.AddListener(SetNewCharacter);
    }

    void SetNewCharacter(GameObject newCharacterObj)
    {
        print("received new character");
        textbox.text = "";
        characterObj = newCharacterObj;
        foreach (TextMeshProUGUI textObj in traitTexts)
        {
            textObj.text = "?";
        }
    }

    void ProcessQuestion(Question question)
    {
        bool didUniqueReply = false; //temporary, will probably remove later
        Character characterData = characterObj.GetComponent<CharacterValues>().CharactersValues;
        for (int i = 0; i < question.traitsRevealed.Count; i++)
        {
            Trait selectedTrait = question.traitsRevealed[i];
            if (characterData.traitList.Contains(selectedTrait))
            {
                print($"Revealed Trait: {selectedTrait}");
                eventCore.revealTraitEV.Invoke(selectedTrait);
            }
        }
        
        for (int i = 0; i < question.replyKey.Count; i++)
        {
            Trait selectedTrait = question.replyKey[i];
            if (characterData.traitList.Contains(selectedTrait))
            {
                print($"Trait: {selectedTrait} \nReply: {question.replyText[i]}");
                textbox.text = question.replyText[i];
                didUniqueReply = true;
                break;
            }
        }

        if (!didUniqueReply)
        {
            print($"Default reply: {question.defaultReply}");
            textbox.text = question.defaultReply;
        }
    }
}
