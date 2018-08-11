using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;
    public float mouseXSpeed;
    public float mouseYSpeed;

    Vector3 velocity;

    Rigidbody rb;
    bool isGrounded = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Camera.main.transform.position = transform.position;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float xAngle = Mathf.Sin(ray.GetPoint(1f).x) * Mathf.Rad2Deg;
        float yAngle = Mathf.Sin(ray.GetPoint(1f).y) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(Mathf.MoveTowardsAngle(transform.eulerAngles.y, yAngle, mouseYSpeed * Time.deltaTime),
                                0,
                                Mathf.MoveTowardsAngle(transform.eulerAngles.x, xAngle, mouseXSpeed * Time.deltaTime));


        velocity = transform.up * speed;
    }

    private void FixedUpdate()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            rb.AddForce(velocity);
    }

    private void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "Wall")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.tag == "Wall")
        {
            isGrounded = false;
        }
    }
}
