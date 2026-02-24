using System.Collections;
using UnityEngine;

public class SelectingLimb : MonoBehaviour
{
    public Limb LookingAtLimb;
    public LimbCharacter LookingAtLimbType;
    public LayerMask CharacerLayer;

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
        Ray MousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(MousePos.origin,Vector3.forward, Color.green);

        Limb hoverLimb = Limb.None;
        LimbCharacter hoverLimbType = LimbCharacter.None;
        if (Physics.Raycast(MousePos, out hit, 100, CharacerLayer))
        {
            LimbClass = hit.collider.gameObject.GetComponent<LimbClassification>();

            if (LimbClass != null)
            {
                LimbClass.Hover = true;
                hoverLimb = LimbClass.Limb;
                hoverLimbType = LimbClass.LimbType;
                if (Input.GetMouseButtonDown(0))
                {
                    PlayerInteraction(hit.collider.gameObject);
                }
            }

            LookingAtLimb = hoverLimb;
            LookingAtLimbType = hoverLimbType;
            print("Hit Player");
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
