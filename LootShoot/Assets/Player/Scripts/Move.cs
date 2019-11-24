using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour //lets player move 
{
    const float speed = 5; //what speed you can move at
    float activeSpeed = 5; //what speed your currently moving at

    public float sensitivity = 2.5f; //sensitivity of mouse
    float xPos; //x position of mouse
    float yPos; //y position of mouse

    private void Start()
    {
        Cursor.visible = false; //can't see mouse
        Cursor.lockState = CursorLockMode.Locked; //mouse is locked to middle of screen
    }

    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Vertical") > 0) //press up key
        {
            transform.Translate(Vector3.forward * Time.deltaTime * activeSpeed); //move up
        }
        if (Input.GetAxisRaw("Vertical") < 0) //down
        {
            transform.Translate(Vector3.back * Time.deltaTime * activeSpeed);
        }
        if (Input.GetAxisRaw("Horizontal") > 0) //right
        {
            transform.Translate(Vector3.right * Time.deltaTime * activeSpeed);
        }
        if (Input.GetAxisRaw("Horizontal") < 0) //left
        {
            transform.Translate(Vector3.left * Time.deltaTime * activeSpeed);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) //start sprint on shift
        {
            activeSpeed = speed * 2; //double speed
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) //end sprint on shift release
        {
            activeSpeed = speed; //reset speed
        }


        //looking, followed a tutorial for this part
        xPos -= Input.GetAxisRaw("Mouse X") * sensitivity * -1;
        yPos += Input.GetAxisRaw("Mouse Y") * sensitivity * -1;

        yPos = Mathf.Clamp(yPos, -90, 90);

        transform.rotation = Quaternion.Euler(yPos, xPos, 0);
    }
}
