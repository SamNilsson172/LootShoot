using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour //lets player move 
{
    public Transform cameraTrans;
    public float sensitivity = 2.5f; //sensitivity of mouse
    float xPos; //x position of mouse
    float yPos; //y position of mouse

    private void Start()
    {
        Cursor.visible = false; //can't see mouse
        Cursor.lockState = CursorLockMode.Locked; //mouse is locked to middle of screen
    }

    void Update()
    {
        //looking, followed a tutorial for this part
        xPos -= Input.GetAxisRaw("Mouse X") * sensitivity * -1;
        yPos += Input.GetAxisRaw("Mouse Y") * sensitivity * -1;

        yPos = Mathf.Clamp(yPos, -90, 90);

        transform.rotation = Quaternion.Euler(yPos, xPos, 0);

        //if (Physics.Raycast(transform.position, cameraTrans.localPosition, out RaycastHit hit, 4)) //spring arm
        //{
        //    camera.position = hit.point;
        //}
    }
}
