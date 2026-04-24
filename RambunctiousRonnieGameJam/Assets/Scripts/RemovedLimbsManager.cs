using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;
using static LimbClassification;

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
        GameObject limbModel = StoredLimb.transform.GetChild(0).gameObject;
        limbModel.GetComponent<MeshFilter>().mesh = Limb.GetComponent<SkinnedMeshRenderer>().sharedMesh;
        limbModel.GetComponent<MeshRenderer>().materials = Limb.GetComponent<SkinnedMeshRenderer>().materials;
        StoredLimb.transform.eulerAngles = new Vector3(-90, 0, 0);
        StoredLimb.tag = Limb.tag;
        limbModel.tag = Limb.tag;
        // set the limb type and limb enum to the one that the player removed
        StoredLimb.GetComponent<LimbClassification>().Limb = Limb.GetComponent<LimbClassification>().Limb;
        StoredLimb.GetComponent<LimbClassification>().LimbType = Limb.GetComponent<LimbClassification>().LimbType;

        BandAidFix(StoredLimb);
    }

    //hardcoded size and rotation fix for specific limbs
    void BandAidFix(GameObject limb)
    {
        LimbClassification limbStats = limb.GetComponent<LimbClassification>();
        GameObject limbModel = limb.transform.GetChild(0).gameObject;
        GameObject limbCollision = limb.transform.GetChild(1).gameObject;

        //heads
        if (limbStats.Limb == global::Limb.head)
        {
            //for the thin head (blue head)
            if (limbStats.LimbType == LimbCharacter.Thin)
            {
                limb.transform.localScale = new Vector3(1.33f, 1.33f, 1.33f);
                limbCollision.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }
        }

        //torso
        if (limbStats.Limb == Limb.torso)
        {
            limb.transform.localEulerAngles = new Vector3(-90, -90, 0);

            if (limbStats.LimbType == LimbCharacter.Thin)
            {
                limb.transform.localScale = new Vector3(5f, 10f, 5f);
                limbCollision.transform.localScale = new Vector3(0.03f, 0.02f, 0.03f);
                return;
            }
        }

        //arms
        if (limbStats.Limb == Limb.rightArm || limbStats.Limb == Limb.leftArm)
        {
            if (limbStats.LimbType == LimbCharacter.Thin)
            {
                limb.transform.localScale = new Vector3(9.75f, 9.75f, 9.75f);
                limb.transform.localEulerAngles = new Vector3(0, 0, 75);
            }

            if (limbStats.LimbType == LimbCharacter.Spiky)
            {
                limb.transform.localScale = new Vector3(4f, 4f, 4f);
            }

            if (limbStats.LimbType == LimbCharacter.Buff)
            {
                limb.transform.localEulerAngles = new Vector3(0, 0, 0); 
            }

            if (limbStats.LimbType == LimbCharacter.Curvy)
            {
                limb.transform.localScale = new Vector3(9.5f, 9.5f, 9.5f);
            }
        }

        //legs
        if (limbStats.Limb == Limb.leftLeg || limbStats.Limb == Limb.rightLeg)
        {
            //for the thin leg
            if (limbStats.LimbType == LimbCharacter.Thin)
            {
                limb.transform.localScale = new Vector3(25f, 25f, 25f);
                limbCollision.transform.localScale = new Vector3(0.008f, 0.008f, 0.008f);
                limb.transform.localEulerAngles = new Vector3(0, -270, 90);
            }

            //for the spiky leg
            if (limbStats.LimbType == LimbCharacter.Spiky)
            {
                limb.transform.localScale = new Vector3(5.34f, 5.34f, 5.34f);
            }

            //for the buff leg
            if (limbStats.LimbType == LimbCharacter.Buff)
            {
                limb.transform.localScale = new Vector3(6f, 6f, 6f);
            }
        }
    }
}
