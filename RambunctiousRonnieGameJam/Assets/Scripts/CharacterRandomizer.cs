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
        public Material[] limbMaterials;
    }

    [Header("Character Data")]
    public GameObject characterPrefab;
    public CharacterLimbs[] possibleLimbs;
    public List<Trait> listOfAllTraits;

    [Header("Character Position")]
    public GameObject spawnPoint;
    public GameObject mainPoint;

    [Header("Miscellanous")]
    [Tooltip("For some reason, the character might be facing the other way. Set this to true to flip them around.")]
    public bool flipRotation;

    EventCore eventCore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (spawnPoint == null)
        {
            spawnPoint = transform.Find("SpawnPoint").gameObject;
            if (spawnPoint == null)
            {
                Debug.LogError("Could not find the spawn point as a child.");
            }
        }

        if (mainPoint == null)
        {
            mainPoint = transform.Find("MainPoint").gameObject;
            if (mainPoint == null)
            {
                Debug.LogError("Could not find the main point as a child.");
            }
        }


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

        Vector3 newPos = spawnPoint.transform.position;
        GameObject characterGameObject = Instantiate(characterPrefab, newPos, Quaternion.identity);
        CharacterValues characterObj = characterGameObject.GetComponent<CharacterValues>();
        characterObj.CharactersValues = Character.CreateInstance<Character>();

        characterObj.CharactersValues.Randomize(listOfAllTraits);
        characterObj.gameObject.name = "Character";
        characterObj.MoveOnScreen(mainPoint.transform.position);
        if (flipRotation)
        {
            characterObj.transform.rotation = new Quaternion(0, 180, 0, 0);
        }

        SetCharacterVisuals(characterObj.CharactersValues, characterGameObject.transform.Find("SpikyFullyRigged").gameObject);
        eventCore.setNewCharacterEV.Invoke(characterGameObject);

    }
    void SetCharacterVisuals(Character characterObj, GameObject randomCharacter)
    {
        for (int i = 0; i < possibleLimbs.Length; i++)
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
                        SkinnedMeshRenderer LimbMesh = randomCharacter.transform.Find("Torso").GetComponent<SkinnedMeshRenderer>();
                        LimbMesh.sharedMesh = possibleLimbs[i].limbSkin;
                        LimbMesh.materials = possibleLimbs[i].limbMaterials;
                    }
                    break;
                case (Limb.leftArm):
                    if (possibleLimbs[i].limbCharacter == characterObj.leftArm)
                    {
                        SkinnedMeshRenderer LimbMesh = randomCharacter.transform.Find("LeftArm").GetComponent<SkinnedMeshRenderer>();
                        LimbMesh.sharedMesh = possibleLimbs[i].limbSkin;
                        LimbMesh.materials = possibleLimbs[i].limbMaterials;
                    }
                    break;
                case (Limb.rightArm):
                    if (possibleLimbs[i].limbCharacter == characterObj.rightArm)
                    {
                        SkinnedMeshRenderer LimbMesh = randomCharacter.transform.Find("RightArm").GetComponent<SkinnedMeshRenderer>();
                        LimbMesh.sharedMesh = possibleLimbs[i].limbSkin;
                        LimbMesh.materials = possibleLimbs[i].limbMaterials;
                    }
                    break;
                case (Limb.leftLeg):
                    if (possibleLimbs[i].limbCharacter == characterObj.leftLeg)
                    {
                        SkinnedMeshRenderer LimbMesh = randomCharacter.transform.Find("LeftLeg").GetComponent<SkinnedMeshRenderer>();
                        LimbMesh.sharedMesh = possibleLimbs[i].limbSkin;
                        LimbMesh.materials = possibleLimbs[i].limbMaterials;
                    }
                    break;
                case (Limb.rightLeg):
                    if (possibleLimbs[i].limbCharacter == characterObj.rightLeg)
                    {
                        SkinnedMeshRenderer LimbMesh = randomCharacter.transform.Find("RightLeg").GetComponent<SkinnedMeshRenderer>();
                        LimbMesh.sharedMesh = possibleLimbs[i].limbSkin;
                        LimbMesh.materials = possibleLimbs[i].limbMaterials;
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
