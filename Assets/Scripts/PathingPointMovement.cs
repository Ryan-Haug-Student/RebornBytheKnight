using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathingPointMovement : MonoBehaviour
{
    [Header("Pathing Points")]
    public Transform player;
    public GameObject point1;
    public GameObject point2;
    public GameObject point3;
    public GameObject point4;

    [Header("Stats")]
    public float speed;
    public float distance;
    public bool movingOut;


    private void Update()
    {
        Vector3 playerPosition = player.position; // Use the player's position as the reference

        if (movingOut)
        {
            // Move points away from the player based on the distance
            point1.transform.position = Vector3.Lerp(point1.transform.position, playerPosition + new Vector3(0, -distance, 0), Time.deltaTime * speed);
            point2.transform.position = Vector3.Lerp(point2.transform.position, playerPosition + new Vector3(-distance, 0, 0), Time.deltaTime * speed);
            point3.transform.position = Vector3.Lerp(point3.transform.position, playerPosition + new Vector3(0, distance, 0), Time.deltaTime * speed);
            point4.transform.position = Vector3.Lerp(point4.transform.position, playerPosition + new Vector3(distance, 0, 0), Time.deltaTime * speed);

            // Check when points are far enough to stop moving out
            if (Vector3.Distance(point1.transform.position, playerPosition + new Vector3(0, -distance, 0)) < 0.1f)
            {
                movingOut = false;
                //Debug.Log("Moving In");
            }
        }

        else
        {
            // Move points back toward the player based on the distance
            point1.transform.position = Vector3.Lerp(point1.transform.position, playerPosition, Time.deltaTime * speed);
            point2.transform.position = Vector3.Lerp(point2.transform.position, playerPosition, Time.deltaTime * speed);
            point3.transform.position = Vector3.Lerp(point3.transform.position, playerPosition, Time.deltaTime * speed);
            point4.transform.position = Vector3.Lerp(point4.transform.position, playerPosition, Time.deltaTime * speed);

            // Check when points are close enough to reverse direction
            if (Vector3.Distance(point1.transform.position, playerPosition) < 0.01f)
            {
                movingOut = true;
                //Debug.Log("Moving Out");
            }
        }
    }
}
