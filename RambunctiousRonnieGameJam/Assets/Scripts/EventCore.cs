using UnityEngine;
using UnityEngine.Events;

public class EventCore : MonoBehaviour
{
    GameManager gameManager;
    
    [HideInInspector]
    //event for creating a character, also randomizing their appearance and traits
    public UnityEvent createNewCharacterEV;

    [HideInInspector]
    //event for setting the new character that was created.
    //for the question processor, success calculator and others that reference the character
    public UnityEvent<GameObject> setNewCharacterEV;

    [HideInInspector]
    //event for asking questions, which will get a reply from character and update traits
    public UnityEvent<Question> askQuestionEV;

    [HideInInspector]
    //event for reaching max questions asked. usually disables all the questions from being asked.
    public UnityEvent reachedMaxQuestionsEV;

    [HideInInspector]
    //event for revealing traits in the character stat menu
    public UnityEvent<Trait> revealTraitEV;

    [HideInInspector]
    //event for changing genres. should happen once the player gets a successful show, which could also be an event in itself
    public UnityEvent changeGenreEV;

    [HideInInspector]
    //event for updating the genre in the character stat menu. follows changeGenreEV
    public UnityEvent<string> updateGenreEV;

    [HideInInspector]
    //event for calculating the success chance. should happen when a new character is created or when a body part is swapped out
    public UnityEvent calculateSuccessChanceEV;

    [HideInInspector]
    //event for updating the success chance in the character stat menu. follows calculateSuccessChanceEV
    public UnityEvent<float> updateSuccessChanceEV;

    [HideInInspector]
    //event for updating the lives in the character stat menu
    public UnityEvent<int> updateLivesAmountEV;

    [HideInInspector]
    //event for updating the character amount in the character stat menu
    public UnityEvent<int> updateCharactersAmountEV;

    [HideInInspector]
    //event for updating the show number in the character stat menu
    public UnityEvent<int> updateShowNumEV;

    //events for approving or denying a character. either checks if show succeeds through chance or begins the salvaging phase
    [HideInInspector]
    public UnityEvent approveCharacterEV; //movement of character to off-screen happens here
    [HideInInspector]
    public UnityEvent followApproveCharacterEV; //event that follows approveCharacterEV. determining if show is successful happens here
    [HideInInspector]
    public UnityEvent denyCharacterEV;

    //event for ending a salvage phase. should be done when character has no body parts and then spawn a new random character
    [HideInInspector]
    public UnityEvent endSalvagePhaseEV;

    //events for succeeding or failing a show. changes either score or lives and should make a new character
    [HideInInspector]
    public UnityEvent successfulShowEV;

    [HideInInspector]
    public UnityEvent failureShowEV;

    //events for winning or losing a game. should happen when exceeding max score or losing all lives
    [HideInInspector]
    public UnityEvent winGameEV;

    [HideInInspector]
    public UnityEvent loseGameEV;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void CreateNewCharacterDelayedInvokeEV(float delay)
    {
        Invoke("CreateNewCharacterInvokeEV", delay);
    }

    public void CreateNewCharacterInvokeEV()
    {
        createNewCharacterEV.Invoke();
    }

    public void ApproveCharacterInvokeEV()
    {
        GameObject oldCharacter = GameObject.Find("OldCharacter");
        if (oldCharacter == null)
        {
            approveCharacterEV.Invoke();
            print("approved character invoked");
        }

    }

    public void DenyCharacterInvokeEV()
    {
        GameObject oldCharacter = GameObject.Find("OldCharacter");
        if (oldCharacter == null && gameManager.charactersLeft > 0)
        {
            print("deny character invoked");
            denyCharacterEV.Invoke();
        }

    }

}
