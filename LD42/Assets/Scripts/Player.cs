using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : DamageableObject {

    public float speed;
    PlayerController pc;
    public Projectile projectilePrefab;
    public GameObject obstaclePrefab;

    Vector2 prevPosition;
    Vector2 currentPosition;


    private void Start()
    {
        pc = GetComponent<PlayerController>();
    }

    private void Update()
    {
        prevPosition = currentPosition;
        currentPosition = transform.position;

        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        pc.Move(inputDir * speed);

        //Space filling mechanic
        Vector2 roundedPrevPos = new Vector2(Mathf.Round(prevPosition.x), Mathf.Round(prevPosition.y));
        Vector2 roundedCurrentPos = new Vector2(Mathf.Round(currentPosition.x), Mathf.Round(currentPosition.y));
        if(Vector2.Distance(roundedPrevPos, roundedCurrentPos) >= 1)
        {
            Instantiate(obstaclePrefab, roundedPrevPos + Vector2.right * .5f, Quaternion.identity);
        }

        //Shooting using the mouse button
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 shootDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized * 1.5f;
            float shootAngle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;

            Debug.Log(shootAngle);
            Projectile projectile = Instantiate(projectilePrefab, (Vector2)transform.position + shootDir,
                                                Quaternion.Euler(shootAngle * Vector3.forward)) as Projectile;

        }
    }

}
