using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : DamageableObject {


    public float speed;
    public int attack;
    public Color deadColor;

    IEnumerator CheckForPlayer()
    {
        while(GameObject.Find("Player") != null)
        {
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Player")
            c.gameObject.GetComponent<Player>().TakeDamage(attack);
    }

    protected override void Die()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = deadColor;
        transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
    }
}
