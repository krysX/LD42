using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    Transform player;
    public float cameraSpeed;

    float halfCameraWidth;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        halfCameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    private void Update()
    {
        if(Mathf.Abs(player.position.x - transform.position.x) < halfCameraWidth || 
            Mathf.Abs(player.position.y -transform.position.y ) < Camera.main.orthographicSize)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, cameraSpeed * Time.deltaTime) + Vector3.back;
        }
    }
}
