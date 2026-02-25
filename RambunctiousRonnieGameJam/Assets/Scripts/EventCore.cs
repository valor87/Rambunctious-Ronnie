using UnityEngine;
using UnityEngine.Events;

public class EventCore : MonoBehaviour
{
    [HideInInspector]
    //event for creating a character, also randomizing their appearance and traits
    public UnityEvent createNewCharacterEV;

    [HideInInspector]
    //event for asking questions, which will get a reply from character and update traits
    public UnityEvent<Question> askQuestionEV;

    [HideInInspector]
    //event for changing genres. should happen once the player gets a successful show, which could also be an event in itself
    public UnityEvent changeGenreEV;

    //event for calculating the success chance. should happen when a new character is created or when a body part is swapped out
    public UnityEvent calculateSuccessChanceEV;

}
