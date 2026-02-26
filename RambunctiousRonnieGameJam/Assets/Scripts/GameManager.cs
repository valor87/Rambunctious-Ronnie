using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Trait;

public class GameManager : MonoBehaviour
{
    public Genres currentShowGenre = Genres.None;
    public int lives = 3;
    public int score = 0;
    public int endScore = 10;

    public int denialAmount = 0;
    public int maxDenials = 10;

    public int questionsAsked = 0;
    public int maxQuestions = 5;

    [Header("References")]
    public GameObject characterInteractionManagerObj;
    public GameObject approveButtonObj;
    public GameObject denyButtonObj;

    EventCore eventCore;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (characterInteractionManagerObj == null)
        {
            characterInteractionManagerObj = GameObject.Find("CharacterInteractionManager");
            if (characterInteractionManagerObj == null)
            {
                Debug.LogError("Could not find character interaction manager.");
            }
            else
            {
                characterInteractionManagerObj.SetActive(false);
            }
        }
        
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.changeGenreEV.AddListener(ChangeGenre);
        eventCore.denyCharacterEV.AddListener(BeginSalvagePhase);
        eventCore.endSalvagePhaseEV.AddListener(EndSalvagePhase);

        eventCore.successfulShowEV.AddListener(IncreaseScore);
        eventCore.successfulShowEV.AddListener(ChangeGenre);
        eventCore.failureShowEV.AddListener(DecreaseLives);

        ChangeGenre();
    }

    void BeginSalvagePhase()
    {
        denialAmount++;
        characterInteractionManagerObj.SetActive(true);
    }

    void EndSalvagePhase()
    {
        characterInteractionManagerObj.SetActive(false);
        eventCore.createNewCharacterEV.Invoke();
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
        score++;
        if (score >= endScore)
        {
            eventCore.winGameEV.Invoke();
            print("YOU WIN!!!!!");
        }
    }  

    void DecreaseLives()
    {
        lives--;
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
