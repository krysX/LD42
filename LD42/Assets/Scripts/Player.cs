using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Rigidbody rb;
    GameObject[] walls;
    bool grounded;

    public float jumpSpeed;
    public float speed;

    public float mouseXspeed;
    public float mouseYspeed;
    public float turnSpeed;

    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Camera.main.transform.localPosition = Vector3.zero;
        walls = GameObject.FindGameObjectsWithTag("Wall");
    }

    private void Update()
    {
        //Mouse X
        float mouseInputX = Input.GetAxisRaw("Mouse X");
        Camera.main.transform.eulerAngles += Vector3.up * mouseInputX * mouseXspeed * Time.deltaTime;
        //Mouse Y
        float mouseInputY = Input.GetAxisRaw("Mouse Y");
        Camera.main.transform.eulerAngles += Vector3.left * mouseInputY * mouseYspeed * Time.deltaTime;
        //Jump
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(jumpSpeed * transform.up);
        }

        if(!grounded)
        { 
            if(rb.velocity != Vector3.zero)
            {
                rb.velocity = speed * transform.forward;
            }

            if(Input.GetMouseButton(0))
            {
                rb.velocity = Vector3.zero;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 0.5f))
                {
                    if (hit.collider.gameObject.tag == "Wall")
                    {
                        transform.position = Vector3.MoveTowards(transform.position, hit.point, speed * Time.deltaTime);
                    }
                }
            }
        }

        //Player rotation
        Vector3 inputRotation = new Vector3(-Input.GetAxisRaw("Vertical"), Input.GetAxis("Horizontal"), 0).normalized;
        transform.eulerAngles += inputRotation * speed * Time.deltaTime;
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
