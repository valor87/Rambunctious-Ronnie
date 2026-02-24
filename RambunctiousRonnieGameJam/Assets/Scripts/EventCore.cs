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

    //function that will invoke askQuestionEV. for the question buttons

}
