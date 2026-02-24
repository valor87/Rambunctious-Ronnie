using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Scriptable Objects/Question")]
public class Question : ScriptableObject
{
    public string questionIdentifier = null;
    public string questionText = null;
    public List<Trait> traitsRevealed = new List<Trait>();
    //a reply for the question that should be very neutral
    public string defaultReply = null;

    //these two lists allow for unique responses based on whether the character has a trait or not
    //it works like a dictionary, but had to split it up into two lists since unity doesn't serialize dictionaries
    //you'll want to use this for the trait that will be revealed, but it can also give hints to other traits but not necessarily reveal
    public List<Trait> replyKey = new List<Trait>();
    public List<string> replyText = new List<string>();
}
