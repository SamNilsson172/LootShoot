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
        if (mf.sharedMesh != Resources.Load<Mesh>(Inventory.loots[0].meshPath))
        {
            mf.sharedMesh = Resources.Load<Mesh>(Inventory.loots[0].meshPath);
            mr.sharedMaterial = Resources.Load<GameObject>(Inventory.loots[0].meshPath).GetComponent<MeshRenderer>().sharedMaterial;
        }
    }
}
