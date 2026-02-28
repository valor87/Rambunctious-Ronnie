using System.Collections;
using UnityEngine;

public class SelectingLimb : MonoBehaviour
{
    [Header("The limb and type the player is looking at")]
    public Limb LookingAtLimb;
    public LimbCharacter LookingAtLimbType;

    [Header("Raycast layers")]
    public LayerMask CharacerLayer;
    public LayerMask ItemLayer;

    public RemovedLimbsManager RemovedLimbs;
    public LimbClassification LimbClass;

    //[HideInInspector]
    public bool salvagePhase = false;

    bool removingALimb;

    // Update is called once per frame
    void Update()
    {
        MoveRayCast();
    }

    void MoveRayCast()
    {
        // setting values
        Ray MouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(MouseRay.origin, Vector3.forward, Color.green);
        Limb hoverLimb = Limb.None;
        LimbCharacter hoverLimbType = LimbCharacter.None;


        if (Physics.Raycast(MouseRay, out hit, 100))
        {
            LimbClass = hit.collider.gameObject.GetComponent<LimbClassification>();

            if (LimbClass == null)
                return;

            hoverLimb = LimbClass.Limb;
            hoverLimbType = LimbClass.LimbType;
            if (salvagePhase)
                LimbClass.Hover = true;

            // the player removes a limb from the body of the character
            if (Input.GetMouseButtonDown(0) && salvagePhase && !removingALimb)
            {
                if (hit.collider.gameObject.layer == 6)
                {
                    removingALimb = true;
                    RemoveLimbInteraction(hit.collider.gameObject);
                }

            }
            // the plaeyr is moving the ui limb
            if (Input.GetMouseButton(0))
            {
                moveLimbFromInventory(hit.collider.gameObject, hit.point);
            }
            // setting the enum of what the player is looking at
            LookingAtLimb = hoverLimb;
            LookingAtLimbType = hoverLimbType;
        }
        

    }

    void moveLimbFromInventory(GameObject LC, Vector3 hit)
    {
        if (LC.layer == 7)
        {

            LC.transform.position = new Vector3(hit.x, hit.y, LC.transform.position.z);
        }
    }
    void RemoveLimbInteraction(GameObject LC)
    {
        StartCoroutine(RemoveLimb(LC));
        LC.GetComponent<LimbClassification>().playRemoveAnimation();
    }
    // wait for the animation to finish
    IEnumerator RemoveLimb(GameObject LC)
    {
        yield return new WaitForSeconds(1);
        LC.gameObject.SetActive(false);
        RemovedLimbs.SeveredLimb(LC);
        removingALimb = false;
    }
}
