using System.Collections.Generic;
using UnityEngine;

public class RemovedLimbsManager : MonoBehaviour
{
    public List<GameObject> limbsOwned = new List<GameObject>();
    public int LimbsOwned;
    public GameObject Instantiate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void SeveredLimb(GameObject Limb)
    {
        // dont let the player have more than 3 limbs
        if(LimbsOwned >= 3)
            return;
        
        limbsOwned.Add(Limb);
        // how many limbs the player has
        LimbsOwned++;
        Vector3 pos = gameObject.transform.Find($"Spot {LimbsOwned}").transform.position;
        // create the limb for the player to move around
        GameObject StoredLimb = Instantiate(Instantiate, pos, Quaternion.identity);
        StoredLimb.tag = Limb.tag;
        // set the limb type and limb enum to the one that the player removed
        StoredLimb.GetComponent<LimbClassification>().Limb = Limb.GetComponent<LimbClassification>().Limb;
        StoredLimb.GetComponent<LimbClassification>().LimbType = Limb.GetComponent<LimbClassification>().LimbType;
    }
}
