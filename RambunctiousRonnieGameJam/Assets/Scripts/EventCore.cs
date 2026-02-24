using UnityEngine;
using UnityEngine.Events;

public class EventCore : MonoBehaviour
{
    [HideInInspector]
    //event for creating a character, also randomizing their appearance and traits
    public UnityEvent createNewCharacterEV;
}
