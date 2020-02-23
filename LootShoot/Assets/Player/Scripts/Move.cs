using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    const float speed = 5; //what speed you can move at
    float activeSpeed = 5; //what speed your currently moving at
    public Transform cameraAngle;
    public Rigidbody rb;
    bool canJump = false;
    public float jumpForece = 5;

    private void Start()
    {
        GetComponent<SaveFunctions>().Load(GetComponent<InventoryInstance>().myInv);
    }

    // Update is called once per frame
    void FixedUpdate()
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

        if (rb.velocity.y < 0 && !canJump)
        {
            Physics.gravity = Vector3.up * -9.8f * 3;
        }
        if (Input.GetButton("Jump") && canJump) //add low jump / dynamic jump
        {
            rb.AddForce(new Vector3(0, jumpForece, 0), ForceMode.Impulse);
            canJump = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            Physics.gravity = new Vector3(0, -9.8f, 0);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }
}
