using System.Collections.Generic;
using UnityEngine;

public class CharacterRandomizer : MonoBehaviour
{
    public GameObject characterPrefab;
    public List<Trait> listOfAllTraits;

    EventCore eventCore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();

        eventCore.createNewCharacterEV.AddListener(RandomizeCharacter);
        RandomizeCharacter();
    }

    //you can test out spawning through the context menu
    [ContextMenu("Spawn and Randomize Character")]
    void RandomizeCharacter()
    {
        CharacterValues characterObj = Instantiate(characterPrefab).GetComponent<CharacterValues>();
        characterObj.CharactersValues = Character.CreateInstance<Character>();

        characterObj.CharactersValues.Randomize(listOfAllTraits);
        
    }
}
