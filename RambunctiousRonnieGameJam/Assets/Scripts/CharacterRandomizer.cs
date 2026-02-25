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
        eventCore.approveCharacterEV.AddListener(RandomizeCharacter);
        eventCore.denyCharacterEV.AddListener(RandomizeCharacter); //temporary for now, remove once swappig body parts is in
    }

    //you can test out spawning through the context menu
    [ContextMenu("Spawn and Randomize Character")]
    void RandomizeCharacter()
    {
        GameObject characterGameObj = GameObject.Find("Character");
        if (characterGameObj != null)
        {
            //renames the character so it'll be separated from the new one
            //pointless rn since it just gets destroyed,
            //but it's here just in case we want to do something with the old character when it gets approved
            //(like just slide it off screen or something)
            characterGameObj.name = "OldCharacter";
            Destroy(characterGameObj);
        }

        Vector3 randomPos = new Vector3(Random.Range(-4, 4), 0.67f, 0);
        CharacterValues characterObj = Instantiate(characterPrefab, randomPos, Quaternion.identity).GetComponent<CharacterValues>();
        characterObj.CharactersValues = Character.CreateInstance<Character>();

        characterObj.CharactersValues.Randomize(listOfAllTraits);
        characterObj.gameObject.name = "Character";
        eventCore.setNewCharacterEV.Invoke(characterObj.gameObject);
    }
}
