using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Scriptable Objects/Character")]
public class Character : ScriptableObject
{
    public enum BodyType
    {
        None = 0,
        Spiky = 1,
        Thin = 2,
        Fit = 3,
        Buff = 4,
        Curvy = 5
    }

    [Header("Body")]
    public BodyType head;
    public BodyType torso;
    public BodyType leftArm;
    public BodyType rightArm;
    public BodyType leftLeg;
    public BodyType rightLeg;
    [Header("Traits")]
    public List<Trait> traitList = new List<Trait>();

    public void Randomize(List<Trait> everyTraitList)
    {
        //would have been better to make a list of the body parts instead of separate variables
        //but oh well
        head = (BodyType)Random.Range(1, 6);
        torso = (BodyType)Random.Range(1, 6);
        leftArm = (BodyType)Random.Range(1, 6);
        rightArm = (BodyType)Random.Range(1, 6);
        leftLeg = (BodyType)Random.Range(1, 6);
        rightLeg = (BodyType)Random.Range(1, 6);

        for (int i = 0; i < 3; i++)
        {
            int randIndex = Random.Range(0, everyTraitList.Count);
            Debug.Log($"{randIndex}, {everyTraitList[randIndex]}");

            //check if character already has selected trait
            if (traitList.Contains(everyTraitList[randIndex]))
            {
                i--;
                continue;
            }

            traitList.Add(everyTraitList[randIndex]);
        }
    }
}
