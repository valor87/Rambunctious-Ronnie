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
        Transform Scale = Instantiate.transform;
        limbsOwned.Add(Limb);
        LimbsOwned++;
        Limb.transform.position = gameObject.transform.Find($"Spot {LimbsOwned}").transform.position;
        GameObject StoredLimb = Instantiate(Instantiate, Limb.transform.position, Quaternion.identity);
        StoredLimb.GetComponent<LimbClassification>().Limb = Limb.GetComponent<LimbClassification>().Limb;
        StoredLimb.GetComponent<LimbClassification>().LimbType = Limb.GetComponent<LimbClassification>().LimbType;
        Limb.transform.localScale = Scale.localScale;
    }
}
