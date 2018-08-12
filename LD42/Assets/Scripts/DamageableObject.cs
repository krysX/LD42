using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject : MonoBehaviour, IDamageable {


    public int health;
    public int startHealth;

	// Use this for initialization
	void Start () {
        health = startHealth;
	}

    public void TakeDamage(int dmg)
    {
        if(health > 0)
        {
            health -= dmg;
            if (health > 0)
            {
                StartCoroutine("PlayDamageEffect");
            }
            else Die();
        }
        else
        {
            Die();
        }
    }

    IEnumerator PlayDamageEffect()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Color originalColor = renderer.color;

        float shiftSpeed = 7f;
        float lerpValue = 0;

        while(lerpValue < 1)
        {
            lerpValue += Time.deltaTime * shiftSpeed;
            float interpolation = (-Mathf.Pow(lerpValue, 2) + lerpValue) * 4;
            renderer.color = Color.Lerp(originalColor, Color.yellow, interpolation);
            yield return null;
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
