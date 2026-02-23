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
    public List<Trait> traitList;
}
