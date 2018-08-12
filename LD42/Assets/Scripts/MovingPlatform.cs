using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingPlatform : MonoBehaviour {

    public float speed;
    public float delay;

    Rigidbody2D rb;
    bool isColliding = false;

    public Vector2 pointA;
    public Vector2 pointB;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine("MoveBetweenPoints");
    }

    IEnumerator MoveBetweenPoints()
    {
        Vector2 destination = pointA;
        Vector2 startingPoint = rb.position;
        yield return new WaitForSeconds(delay);
        

        while(true)
        {
            if(rb.position == startingPoint + pointA)
            {
                destination = pointB;
                Debug.Log("at pointA heading pointB");
            }
            else if (rb.position == startingPoint + pointB)
            {
                destination = pointA;
                Debug.Log("at pointB heading pointA");
            }

            if(isColliding)
            {
                if(destination == pointA)
                {
                    destination = pointB;
                    pointA = rb.position - startingPoint;
                }
                else if(destination == pointB)
                {
                    destination = pointA;
                    pointB = rb.position - startingPoint;
                }
            }
            
            rb.MovePosition(Vector2.MoveTowards(rb.position, startingPoint + destination, speed * Time.fixedDeltaTime));
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        isColliding = true;
        Debug.Log(isColliding);
    }

    private void OnCollisionExit2D(Collision2D c)
    {
        isColliding = false;
        Debug.Log(isColliding);
    }
}
