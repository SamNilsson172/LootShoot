using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOverMe : MonoBehaviour
{
    public float height;
    // Start is called before the first frame update
    void Awake()
    {
        if (Physics.Raycast(transform.position + Vector3.up * 50, Vector3.down, out RaycastHit hit))
        {
            if (hit.point.y > 20)
                gameObject.layer = 8;
            height = hit.point.y;
        }

    }
}
