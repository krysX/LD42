using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour {

    public float startingSpeed;
    public int damage;

    Rigidbody2D rb;
    Vector2 velocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity = startingSpeed * transform.right;
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if(c.gameObject.tag == "Enemy")
        {
            c.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        rb.MovePosition(((Vector2)transform.position) + velocity * Time.fixedDeltaTime);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right * startingSpeed, startingSpeed * Time.fixedDeltaTime);

        if(hit.collider.gameObject != null)
        {
            Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                hit.collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }

            Destroy(gameObject);
        }
        
        
    }
}
