using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionProcessor : MonoBehaviour
{
    EventCore eventCore;
    SuccessCalculator successCalculator;

    public GameObject characterObj;
    public TextMeshProUGUI textbox;
    public TextMeshProUGUI[] traitTexts = new TextMeshProUGUI[3];
    public AudioClip[] soundEffects = new AudioClip[3];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.askQuestionEV.AddListener(ProcessQuestion);
        eventCore.setNewCharacterEV.AddListener(SetNewCharacter);

        successCalculator = GameObject.Find("SuccessCalculator").GetComponent<SuccessCalculator>();
    }

    void SetNewCharacter(GameObject newCharacterObj)
    {
        print("received new character");
        textbox.text = "";
        characterObj = newCharacterObj;
        foreach (TextMeshProUGUI textObj in traitTexts)
        {
            textObj.text = "?";
            textObj.transform.GetChild(0).GetComponent<Image>().color = new Color32(137, 137, 137, 100);
        }
    }

    void ProcessQuestion(Question question)
    {
        bool didUniqueReply = false;
        bool revealedTrait = false;
        Character characterData = characterObj.GetComponent<CharacterValues>().CharactersValues;
        for (int i = 0; i < question.traitsRevealed.Count; i++)
        {
            Trait selectedTrait = question.traitsRevealed[i];
            if (characterData.traitList.Contains(selectedTrait))
            {
                print($"Revealed Trait: {selectedTrait}");
                revealedTrait = true;

                //determine if the trait is positive or negative based on the genre
                int traitType = 0;

                if (successCalculator.CheckPositiveTrait(selectedTrait))
                {
                    traitType = 1;
                }
                else if (successCalculator.CheckNegativeTrait(selectedTrait))
                {
                    traitType = 2;
                }

                eventCore.revealTraitEV.Invoke(selectedTrait, traitType);
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

        if (revealedTrait)
            eventCore.playOneShotEV.Invoke(soundEffects[2]);
        else if (didUniqueReply)
            eventCore.playOneShotEV.Invoke(soundEffects[1]);
        else
            eventCore.playOneShotEV.Invoke(soundEffects[0]);
    }
}
