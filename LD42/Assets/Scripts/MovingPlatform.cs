using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingPlatform : MonoBehaviour {

    public float speed;

    Rigidbody2D rb;

    Vector2 pointA;
    Vector2 pointB;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        pointA = GameObject.Find("Point A").transform.position;
        pointB = GameObject.Find("Point B").transform.position;

        StartCoroutine("MoveBetweenPoints");
        
    }

    IEnumerator MoveBetweenPoints()
    {
        rb.position = pointA;
        Vector2 destination = new Vector2();

        while(true)
        {
            if(rb.position == pointA)
            {
                destination = pointB;
            }
            else if (rb.position == pointB)
            {
                destination = pointA;
            }

            Debug.Log(destination);
            
            rb.MovePosition(Vector2.MoveTowards(rb.position, destination, speed * Time.fixedDeltaTime));
            yield return new WaitForFixedUpdate();
        }
    }
}
