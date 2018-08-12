using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))] 
public class Player : MonoBehaviour {

    public float speed;
    Rigidbody2D rb; 

    public int startingSpaces;
    int spaces;
    int clearableObstacles;

    public Text spacesText;
    public Text clearableObstaclesText;

    public GameObject obstaclePrefab;
    GameObject obstacle;
    AudioSource fxSource;
    Vector2 prevPosition;
    Vector2 currentPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fxSource = GetComponent<AudioSource>();
        spaces = startingSpaces;
        currentPosition = transform.position;
    }


    private void Update()
    {
        prevPosition = currentPosition;
        currentPosition = transform.position;

        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rb.velocity = inputDir * speed;

        //Space filling mechanic
        Vector2 roundedPrevPos = new Vector2(Mathf.Round(prevPosition.x), Mathf.Round(prevPosition.y));
        Vector2 roundedCurrentPos = new Vector2(Mathf.Round(currentPosition.x), Mathf.Round(currentPosition.y));
        if(Vector2.Distance(roundedPrevPos, roundedCurrentPos) >= 1)
        {
            obstacle = Instantiate(obstaclePrefab, roundedPrevPos, Quaternion.identity);
            obstacle.transform.parent  = GameObject.Find("Tilemap").transform;
        }

        if(Input.GetKeyDown(KeyCode.Space) && spaces > 0)
        {
            spaces--;
            clearableObstacles += 5;
        }

        clearableObstaclesText.text = clearableObstacles.ToString();
        spacesText.text = spaces.ToString();

    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Obstacle" && clearableObstacles > 0)
        {
            clearableObstacles--;
            fxSource.Play();
            Destroy(c.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Space")
        {
            spaces++;
            Destroy(c.gameObject);
        }
        else if (c.gameObject.tag == "Lava")
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("dead lol");
    }

    void  Win()
    {

    }
}
