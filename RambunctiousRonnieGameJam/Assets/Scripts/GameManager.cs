using UnityEngine;
using static Trait;

public class GameManager : MonoBehaviour
{
    public Genres currentShowGenre = Genres.None;
    public int lives = 3;
    public int score = 0;
    public int endScore = 10;

    EventCore eventCore;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.changeGenreEV.AddListener(changeGenre);

        changeGenre();
    }

    void changeGenre()
    {
        while (true)
        {
            Genres randomGenre = (Genres)Random.Range(0, 13);
            if (randomGenre != currentShowGenre)
            {
                currentShowGenre = randomGenre;
                return;
            }
        }
    }
}
