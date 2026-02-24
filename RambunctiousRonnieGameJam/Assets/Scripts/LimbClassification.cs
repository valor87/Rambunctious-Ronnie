using UnityEngine;
public enum LimbCharacter
{
    None,
    Spiky,
    Thin,
    Fit,
    Buff,
    Curvy,
}
public enum Limb
{
    None,
    head,
    torso,
    leftArm,
    rightArm,
    leftLeg,
    rightLeg,
}
public class LimbClassification : MonoBehaviour
{
    public Limb Limb;
    public LimbCharacter LimbType;
}
