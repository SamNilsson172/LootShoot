using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    const float speed = 5; //what speed you can move at
    float activeSpeed = 5; //what speed your currently moving at
    public Transform cameraAngle;
    public Rigidbody rb;
    bool canJump = true;
    public float jumpForece = 5;

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, cameraAngle.eulerAngles.y, 0);

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

        if (Input.GetButtonDown("Jump") && canJump)
        {
            rb.AddForce(new Vector3(0, jumpForece, 0), ForceMode.Impulse);
            canJump = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }

    }
}
