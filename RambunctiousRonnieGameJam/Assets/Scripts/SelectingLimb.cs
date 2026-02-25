using System.Collections;
using UnityEngine;

public class SelectingLimb : MonoBehaviour
{
    public Limb LookingAtLimb;
    public LimbCharacter LookingAtLimbType;
    public LayerMask CharacerLayer;
    public LayerMask ItemLayer;

    public RemovedLimbsManager RemovedLimbs;
    public LimbClassification LimbClass;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveRayCast();
    }

    void MoveRayCast()
    {
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
            LimbClass.Hover = true;

            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.gameObject.layer == 6)
                {

                    PlayerInteraction(hit.collider.gameObject);
                }
                
            }

            if (Input.GetMouseButton(0))
            {
                if (hit.collider.gameObject.layer == 7)
                {

                    hit.collider.gameObject.transform.position
                        = new Vector3(hit.point.x, hit.point.y, hit.collider.gameObject.transform.position.z);
                }
            }
            LookingAtLimb = hoverLimb;
            LookingAtLimbType = hoverLimbType;
        }
        else
        {
            if (LimbClass != null)
            {
                LimbClass.Hover = false;
            }
        }
    }

    void PlayerInteraction(GameObject LC)
    {
        StartCoroutine(RemoveLimb(LC));
        LC.GetComponent<LimbClassification>().playRemoveAnimation();
    }

    IEnumerator RemoveLimb(GameObject LC)
    {
        yield return new WaitForSeconds(1);
        RemovedLimbs.SeveredLimb(LC);
    }
}
