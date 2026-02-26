using System.Globalization;
using Unity.VisualScripting;
using UnityEditor.UIElements;
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
        if (isTrash)
            GetRidOfLimb(other.gameObject);

        if (other.gameObject.CompareTag(tagLookingFor))
        {
            print($"Found an object with a {tagLookingFor}");
            LimbClassification limbData = limb.GetComponent<LimbClassification>();
            limbData.LimbType = other.gameObject.GetComponent<LimbClassification>().LimbType;

            GetRidOfLimb(other.gameObject);
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
