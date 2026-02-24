using System;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CharacterValues : MonoBehaviour  
{
    public Character CharactersValues;
    public List<GameObject> ChildObjectsBodyParts;
    string type;
    private void Start()
    {
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

    // puts all the children of an object into a list
    void SetChildrenToAList(GameObject Parent, List<GameObject> Children)
    {
        int ChildrenCount = Parent.transform.childCount;

        for (int i = 0; i < ChildrenCount; i++)
        {
            GameObject ChildAtIndex = Parent.transform.GetChild(i).gameObject;
            ChildObjectsBodyParts.Add(ChildAtIndex);
        }
    }
}
