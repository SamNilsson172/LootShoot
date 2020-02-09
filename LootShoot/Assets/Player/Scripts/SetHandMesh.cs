using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHandMesh : MonoBehaviour
{
    public MeshFilter mf;
    public MeshRenderer mr;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Inventory.loots.Count > 0) //if inventory is not empty
            if (mf.sharedMesh != Resources.Load<Mesh>(Inventory.loots[0].meshPath)) //and the hand mesh is not the same as the first lot in inventory
            {
                mf.sharedMesh = Resources.Load<Mesh>(Inventory.loots[0].meshPath); //set hand mesh to first inv slot
                mr.sharedMaterial = Resources.Load<GameObject>(Inventory.loots[0].meshPath).GetComponent<MeshRenderer>().sharedMaterial;
            }
    }
}
