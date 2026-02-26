using UnityEngine;

public class StopBoneAnimation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void StopAnimation()
    {
        animator.SetBool("Hover", false);
    }
    
}
