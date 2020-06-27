
using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float speed;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void Start()
    {
        transform.position = playerTransform.position;//Player and camera are at the same position at the start of game
    }

    private void Update()
    {
        if (playerTransform != null)//Checks to make sure if player transform exists(because player can die)
        {
            float clampedX = Mathf.Clamp(playerTransform.position.x, minX, maxX);//This is a variable which clamps based off of minx and maxX
            float clampedY = Mathf.Clamp(playerTransform.position.y, minY, maxY);

            transform.position = Vector2.Lerp(transform.position, new Vector2(clampedX, clampedY), speed);//Moves camera from 1 point to another based on speed
        }
    }
}
