using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : DamageableObject {

    Rigidbody2D rb;
    Vector2 velocity;

    public Text spaceText;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 _velocity)
    {
        velocity = _velocity;
    }

    private void FixedUpdate()
    {
        rb.MovePosition((Vector2)transform.position + velocity * Time.fixedDeltaTime);
    }
}
