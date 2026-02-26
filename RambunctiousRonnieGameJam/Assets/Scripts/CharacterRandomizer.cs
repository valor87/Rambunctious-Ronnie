using System.Collections.Generic;
using UnityEngine;


public class CharacterRandomizer : MonoBehaviour
{
    [System.Serializable]
    public struct CharacterLimbs
    {
        public string limbName;
        public Limb limbType;
        public BodyType limbCharacter;
        public Mesh limbSkin;
        public List<Material> limbMaterials;
    }

    public CharacterLimbs[] possibleLimbs;
    public GameObject characterPrefab;
    public List<Trait> listOfAllTraits;

    EventCore eventCore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();

        eventCore.createNewCharacterEV.AddListener(RandomizeCharacter);
        eventCore.followApproveCharacterEV.AddListener(RandomizeCharacter);
        eventCore.denyCharacterEV.AddListener(ShiftCharacterToOld);
    }
   
    //you can test out spawning through the context menu
    [ContextMenu("Spawn and Randomize Character")]
    public void RandomizeCharacter()
    {
        ShiftCharacterToOld();

        Vector3 randomPos = new Vector3(Random.Range(-0.7f, 0.7f), 1.2f, -8); //should change to a set position once game is more developed
        GameObject characterGameObject = Instantiate(characterPrefab, randomPos, Quaternion.identity);
        CharacterValues characterObj = characterGameObject.GetComponent<CharacterValues>();
        characterObj.CharactersValues = Character.CreateInstance<Character>();

        characterObj.CharactersValues.Randomize(listOfAllTraits);
        characterObj.gameObject.name = "Character";

        SetCharacterVisuals(characterObj.CharactersValues, characterGameObject.transform.Find("SpikyFullyRigged").gameObject);
        eventCore.setNewCharacterEV.Invoke(characterGameObject);

    }
    void SetCharacterVisuals(Character characterObj, GameObject randomCharacter)
    {
        for(int i = 0; i < possibleLimbs.Length; i++)
        {
            switch (possibleLimbs[i].limbType)
            {
                case (Limb.head):
                    if (possibleLimbs[i].limbCharacter == characterObj.head)
                    {
                        SkinnedMeshRenderer LimbMesh = randomCharacter.transform.Find("Head").GetComponent<SkinnedMeshRenderer>();
                        LimbMesh.sharedMesh = possibleLimbs[i].limbSkin;
                        LimbMesh.materials = possibleLimbs[i].limbMaterials;

                    }
                    break;
                case (Limb.torso):
                    if (possibleLimbs[i].limbCharacter == characterObj.torso)
                    {
                        randomCharacter.transform.Find("Torso").GetComponent<SkinnedMeshRenderer>().sharedMesh = possibleLimbs[i].limbSkin;
                    }
                    break;
                case (Limb.leftArm):
                    if (possibleLimbs[i].limbCharacter == characterObj.leftArm)
                    {
                        randomCharacter.transform.Find("LeftArm").GetComponent<SkinnedMeshRenderer>().sharedMesh = possibleLimbs[i].limbSkin;
                    }
                    break;
                case (Limb.rightArm):
                    if (possibleLimbs[i].limbCharacter == characterObj.rightArm)
                    {
                        randomCharacter.transform.Find("RightArm").GetComponent<SkinnedMeshRenderer>().sharedMesh = possibleLimbs[i].limbSkin;
                    }
                    break;
                case (Limb.leftLeg):
                    if (possibleLimbs[i].limbCharacter == characterObj.leftLeg)
                    {
                        randomCharacter.transform.Find("LeftLeg").GetComponent<SkinnedMeshRenderer>().sharedMesh = possibleLimbs[i].limbSkin;
                    }
                    break;
                case (Limb.rightLeg):
                    if (possibleLimbs[i].limbCharacter == characterObj.rightLeg)
                    {
                        randomCharacter.transform.Find("RightLeg").GetComponent<SkinnedMeshRenderer>().sharedMesh = possibleLimbs[i].limbSkin;
                    }
                    break;
            }
            
        }
    }
    void ShiftCharacterToOld()
    {
        GameObject characterGameObj = GameObject.Find("Character");
        if (characterGameObj != null)
        {
            //renames the character so it'll be separated from the new one
            //pointless rn since it just gets destroyed,
            //but it's here just in case we want to do something with the old character when it gets approved
            //(like just slide it off screen or something)
            characterGameObj.name = "OldCharacter";
            //Destroy(characterGameObj);
        }
    }
}
