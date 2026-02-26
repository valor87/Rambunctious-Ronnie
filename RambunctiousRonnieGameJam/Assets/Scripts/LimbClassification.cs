using System.Collections;
using UnityEngine;
public enum LimbCharacter
{
    None,
    Spiky,
    Thin,
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
    public bool Hover;
    public Limb Limb;
    public LimbCharacter LimbType;
    public Animator boneAnimator;

    private void Start()
    {
        StartCoroutine(TurnHoverOff());
    }

    private void Update()
    {
        if (boneAnimator == null)
            return;
        boneAnimator.SetBool("Hover", Hover);
        
    }

    public void StopAnimation()
    {
        Hover = false;
        boneAnimator.SetBool("Hover", false);
    }

    public void playRemoveAnimation()
    {
        StopAnimation();
        boneAnimator.SetTrigger("Remove");
    }

    public void DestroyLimb()
    {
        this.gameObject.SetActive(false);
    }
    IEnumerator TurnHoverOff()
    {
        while (true)
        {
            yield return new WaitForSeconds(.5f);
            Hover = false;
        }
    }
}
