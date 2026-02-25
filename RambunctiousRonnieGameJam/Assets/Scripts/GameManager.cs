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

    [Header("References")]
    public TextMeshProUGUI genreTitle;
    public GameObject approveButtonObj;
    public GameObject denyButtonObj;

    EventCore eventCore;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.changeGenreEV.AddListener(changeGenre);
        eventCore.successfulShowEV.AddListener(increaseScore);
        eventCore.successfulShowEV.AddListener(changeGenre);
        eventCore.failureShowEV.AddListener(decreaseLives);

        changeGenre();
    }

    void changeGenre()
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

        genreTitle.text = $"Genre: {currentShowGenre}";
    }

    void increaseScore()
    {
        score++;
        if (score >= endScore)
        {
            eventCore.winGameEV.Invoke();
            print("YOU WIN!!!!!");
        }
    }  

    void decreaseLives()
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
