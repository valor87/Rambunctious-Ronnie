using UnityEngine;

public class SelectingLimb : MonoBehaviour
{
    public LayerMask CharacerLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveRayCast();
    }

    void MoveRayCast()
    {
        Ray MousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit;
        Debug.DrawRay(MousePos.origin,Vector3.forward, Color.green);
        if (Physics.Raycast(MousePos,100,CharacerLayer))
        {
            print("Hit Player");
        }
    }

    void PlayerInteraction()
    {

    }
}
