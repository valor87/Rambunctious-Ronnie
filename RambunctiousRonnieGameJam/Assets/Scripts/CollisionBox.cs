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
            Destroy(other.gameObject);
        if (other.gameObject.CompareTag(tagLookingFor))
        {
            print($"Found an object with a {tagLookingFor}");
            Destroy(other.gameObject);
            RemovableLimbManager.limbsOwned.Remove(other.gameObject);
            RemovableLimbManager.LimbsOwned--;
            limb.SetActive(true);
            return;
        }

        print($"Found a limb but its not a {tagLookingFor}");
    }
  
}
