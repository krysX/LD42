using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))] 
public class Player : MonoBehaviour {

    public float speed;
    Rigidbody2D rb;
    PlayerCollision playerCollision;

    public int startingSpaces;
    int spaces;
    int clearableObstacles;

    public Text spacesText;
    public Text clearableObstaclesText;

    public GameObject obstaclePrefab;
    public float obstacleAnimSpeed;

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
            ObstacleAppearAnimation obstAnim = obstacle.AddComponent<ObstacleAppearAnimation>();
            obstAnim.SetSpeed(obstacleAnimSpeed);
        }

        if(Input.GetKeyDown(KeyCode.Space) && spaces > 0)
        {
            spaces--;
            clearableObstacles += 5;
        }

        if(clearableObstaclesText != null && spacesText != null)
        {
            clearableObstaclesText.text = clearableObstacles.ToString();
            spacesText.text = spaces.ToString();
        }
        

        if (IsWalled())
            Die();

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
        else if (c.gameObject.tag == "End")
        {
            Win();
        }
    }

    bool IsWalled()
    {
        RaycastHit2D[] hits = new RaycastHit2D[4];
        hits[0] = Physics2D.Raycast((Vector2)transform.position + Vector2.up * .5f, (Vector2)transform.position + Vector2.up, 1f);
        hits[1] = Physics2D.Raycast((Vector2)transform.position + Vector2.down * .5f, (Vector2)transform.position + Vector2.down, 1f);
        hits[2] = Physics2D.Raycast((Vector2)transform.position + Vector2.left * .5f, (Vector2)transform.position + Vector2.left, 1f);
        hits[3] = Physics2D.Raycast((Vector2)transform.position + Vector2.right * .7f, (Vector2)transform.position + Vector2.right, 1f);

        bool colliding = true;
        foreach(RaycastHit2D hit in hits)
        {
            colliding &= hit.collider != null;
        }
        
        if(colliding)
        {
            bool result = true;
            foreach(RaycastHit2D hit in hits)
            {
                result &= hit.collider.tag == "Obstacle" || hit.collider.tag == "MapBorder";
            }

            result &= spaces <= 0 && clearableObstacles <= 0;
            return result;
        }
        return false;
    }
        void Die()
    {
        GameController gc = GameObject.Find("Game Controller").GetComponent<GameController>();
        gc.GameOver();
    }

    void  Win()
    {
        GameController gc = GameObject.Find("Game Controller").GetComponent<GameController>();
        gc.Victory();
    }
}
