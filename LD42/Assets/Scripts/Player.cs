using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Rigidbody rb;
    bool grounded;

    public float jumpVelocity;
    public float speed;

    public float mouseXspeed;
    public float mouseYspeed;

    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Camera.main.transform.localPosition = Vector3.zero;
    }

    private void Update()
    {
        //Mouse X
        float mouseInputX = Input.GetAxisRaw("Mouse X");
        transform.eulerAngles += Vector3.up * mouseInputX * mouseXspeed * Time.deltaTime;
        //Mouse Y
        float mouseInputY = Input.GetAxisRaw("Mouse Y");
        Camera.main.transform.eulerAngles += Vector3.left * mouseInputY * mouseYspeed * Time.deltaTime;
        //Jump
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(jumpVelocity * transform.up);
        }

        //Player Movement
        Vector3 inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        transform.Translate(inputDir * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision c)
    {
        if(c.gameObject.tag == "Wall")
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision c)
    {
        if (c.gameObject.tag == "Wall")
        { 
            grounded = false;
        }
    }

}
