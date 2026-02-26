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
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (animator == null)
            return;
        animator.SetBool("Hover", Hover);
        
    }

    public void StopAnimation()
    {
        Hover = false;
        animator.SetBool("Hover", false);
    }

    public void playRemoveAnimation()
    {
        StopAnimation();
        animator.SetTrigger("Remove");
    }

    public void DestroyLimb()
    {
        this.gameObject.SetActive(false);
    }
}
