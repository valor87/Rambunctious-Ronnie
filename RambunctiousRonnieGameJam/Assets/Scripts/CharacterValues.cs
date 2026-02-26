using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterValues : MonoBehaviour  
{
    [Header("Important Data")]
    public Character CharactersValues;
    public GameObject characterHitboxes;
    public List<GameObject> ChildObjectsBodyParts;

    [Header("Miscellanous")]
    public float moveSpeed = 2f;
    public float deleteTimer = 2f;
    EventCore eventCore; 
    string type;
    private void Start()
    {
        characterHitboxes = transform.Find("CharacterHitBoxes").gameObject;
        
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.approveCharacterEV.AddListener(MoveOffScreen);
        eventCore.denyCharacterEV.AddListener(DisableHitboxes);
        
        // gets the list of the children
        SetChildrenToAList(this.gameObject, ChildObjectsBodyParts);
        int index = 1;

        // sets limbValue and limbType
        foreach (GameObject childObject in ChildObjectsBodyParts)
        {
            LimbClassification LimbClass = childObject.GetComponent<LimbClassification>();

            if (Enum.IsDefined(typeof(Limb), 1))
            {
                LimbClass.Limb = (Limb)index;

                switch (index)
                {
                    case 1:
                        LimbClass.LimbType = (LimbCharacter)(int)CharactersValues.head;
                        break;
                    case 2:
                        LimbClass.LimbType = (LimbCharacter)(int)CharactersValues.torso;
                        break;
                    case 3:
                        LimbClass.LimbType = (LimbCharacter)(int)CharactersValues.leftArm;
                        break;
                    case 4:
                        LimbClass.LimbType = (LimbCharacter)(int)CharactersValues.rightArm;
                        break;
                    case 5:
                        LimbClass.LimbType = (LimbCharacter)(int)CharactersValues.leftLeg;
                        break;
                    case 6:
                        LimbClass.LimbType = (LimbCharacter)(int)CharactersValues.rightLeg;
                        break;
                    default:
                        Debug.Log("I have no idea what broke but it did mb");
                        break;
                }

                index++;
            }
        }
    }

    private void Update()
    {
        if (CheckIfAllBodyPartsGone())
        {
            eventCore.endSalvagePhaseEV.Invoke();
            Destroy(gameObject);
        }
    }

    // puts all the children of an object into a list
    void SetChildrenToAList(GameObject Parent, List<GameObject> Children)
    {
        int ChildrenCount = Parent.transform.childCount;

        for (int i = 0; i < ChildrenCount; i++)
        {
            GameObject ChildAtIndex = Parent.transform.GetChild(i).gameObject;
            if (!ChildAtIndex.CompareTag("Untagged"))
            {
                ChildObjectsBodyParts.Add(ChildAtIndex);
            }
            
        }
    }

    bool CheckIfAllBodyPartsGone()
    {
        foreach (GameObject bodyPart in ChildObjectsBodyParts)
        {
            if (bodyPart.activeSelf)
                return false;
        }

        return true;
    }

    void DisableHitboxes()
    {
        characterHitboxes.SetActive(false);
    }

    void MoveOffScreen()
    {
        characterHitboxes.SetActive(false);
        StartCoroutine(MovingOffScreen());
    }

    IEnumerator MovingOffScreen()
    {
        Vector3 newPos = transform.position;
        float timer = 0f;
        while (timer < deleteTimer)
        {
            newPos.x += moveSpeed * Time.deltaTime;
            transform.position = newPos;
            timer += Time.deltaTime;
            yield return Time.deltaTime;
        }

        eventCore.followApproveCharacterEV.Invoke();
        Destroy(gameObject);
        yield return null;
        
    }
}
