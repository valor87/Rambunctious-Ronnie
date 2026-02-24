using System.Collections.Generic;
using UnityEngine;

public class RemovedLimbsManager : MonoBehaviour
{
    public List<GameObject> limbsOwned = new List<GameObject>();
    int LimbsOwned;
    public GameObject Instantiate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SeveredLimb(GameObject Limb)
    {
        if(LimbsOwned >= 3)
            return;
        limbsOwned.Add(Limb);
        LimbsOwned++;
        Vector3 pos = gameObject.transform.Find($"Spot {LimbsOwned}").transform.position;
        GameObject StoredLimb = Instantiate(Instantiate, pos, Quaternion.identity);
        StoredLimb.GetComponent<LimbClassification>().Limb = Limb.GetComponent<LimbClassification>().Limb;
        StoredLimb.GetComponent<LimbClassification>().LimbType = Limb.GetComponent<LimbClassification>().LimbType;
    }
}
