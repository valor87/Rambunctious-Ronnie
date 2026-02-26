using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Trait;

public class GameManager : MonoBehaviour
{
    public Genres currentShowGenre = Genres.None;
    public int lives = 3;
    public int showNum = 0;
    public int showEndNum = 10;

    public int startingCharactersNum = 10;
    public int charactersLeft;

    public int questionsAsked = 0;
    public int maxQuestions = 5;

    [Header("References")]
    public SelectingLimb characterInteractionManagerObj;
    public GameObject approveButtonObj;
    public GameObject denyButtonObj;

    EventCore eventCore;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        charactersLeft = startingCharactersNum;
        
        if (characterInteractionManagerObj == null)
        {
            characterInteractionManagerObj = GameObject.Find("CharacterInteractionManager").GetComponent<SelectingLimb>();
            if (characterInteractionManagerObj == null)
            {
                Debug.LogError("Could not find character interaction manager.");
            }
            else
            {
                //characterInteractionManagerObj.SetActive(false);
            }
        }
        
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.changeGenreEV.AddListener(ChangeGenre);
        eventCore.denyCharacterEV.AddListener(BeginSalvagePhase);
        eventCore.endSalvagePhaseEV.AddListener(EndSalvagePhase);
        eventCore.approveCharacterEV.AddListener(ResetCharactersAmount);

        eventCore.successfulShowEV.AddListener(IncreaseScore);
        eventCore.successfulShowEV.AddListener(ChangeGenre);
        eventCore.failureShowEV.AddListener(DecreaseLives);

        ChangeGenre();
    }

    void BeginSalvagePhase()
    {
        charactersLeft--;
        eventCore.updateCharactersAmountEV.Invoke(charactersLeft);
        characterInteractionManagerObj.salvagePhase = true;
    }

    void EndSalvagePhase()
    {
        characterInteractionManagerObj.salvagePhase = false;
        eventCore.createNewCharacterEV.Invoke();
    }

    void ResetCharactersAmount()
    {
        charactersLeft = startingCharactersNum;
        eventCore.updateCharactersAmountEV.Invoke(charactersLeft);
    }

    void ChangeGenre()
    {
        while (true)
        {
            Genres randomGenre = (Genres)Random.Range(1, 13);
            if (randomGenre != currentShowGenre)
            {
                currentShowGenre = randomGenre;
                break;
            }
        }

        eventCore.updateGenreEV.Invoke(currentShowGenre.ToString());
    }

    void IncreaseScore()
    {
        showNum++;
        eventCore.updateShowNumEV.Invoke(showNum);
        if (showNum >= showEndNum)
        {
            eventCore.winGameEV.Invoke();
            print("YOU WIN!!!!!");
        }
    }  

    void DecreaseLives()
    {
        lives--;
        eventCore.updateLivesAmountEV.Invoke(lives);

        if (lives <= 0)
        {
            eventCore.loseGameEV.Invoke();

            //it would be better for the buttons to disable themself by listening to the loseGameEV
            //but i'll have to make a new class for them and kinda lazy rn
            approveButtonObj.SetActive(false);
            denyButtonObj.SetActive(false);

            print("YOU LOSE!!!!!");
        }
    }
}
