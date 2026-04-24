using UnityEngine;

public class CollisionBox : MonoBehaviour
{
    [Header("The tag that the box is looking for, the body part")]
    public string tagLookingFor;
    [Header("The limb that this is working for")]
    public GameObject limb;
    public bool isTrash = false;
    RemovedLimbsManager RemovableLimbManager;

    
    private void Start()
    {
        RemovableLimbManager = GameObject.Find("RemovedLimbManager").GetComponent<RemovedLimbsManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject uiLimb = other.transform.parent.gameObject;
        
        if (isTrash)
        {
            GetRidOfLimb(uiLimb);
            return;
        }
            

        if (uiLimb.CompareTag(tagLookingFor))
        {
            GameObject uiLimbModel = uiLimb.transform.GetChild(0).gameObject;

            print($"Found an object with a {tagLookingFor}");
            LimbClassification limbData = limb.GetComponent<LimbClassification>();
            limbData.LimbType = uiLimb.GetComponent<LimbClassification>().LimbType;

            // swap the mesh
            limb.GetComponent<SkinnedMeshRenderer>().sharedMesh = uiLimbModel.GetComponent<MeshFilter>().mesh;
            limb.GetComponent<SkinnedMeshRenderer>().materials = uiLimbModel.GetComponent<MeshRenderer>().materials;
            Debug.LogError("Set the mesh");
            GetRidOfLimb(uiLimb);
            limb.SetActive(true); //might be pointless later since swapping body parts only work when character has all of their body parts
            limb.GetComponent<LimbClassification>().Hover = false;
            return;
        }

        print($"Found a limb but its not a {tagLookingFor}");
    }

    void GetRidOfLimb(GameObject Limb)
    {
        Destroy(Limb);
        RemovableLimbManager.limbsOwned.Remove(Limb.gameObject);
        RemovableLimbManager.LimbsOwned--;
    }
}
