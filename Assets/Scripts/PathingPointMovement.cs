using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathingPointMovement : MonoBehaviour
{
    [Header("Pathing Points")]
    public GameObject point1;
    public GameObject point2;
    public GameObject point3;
    public GameObject point4;

    [Header("stats")]
    public float speed;
    public float delay;
    public float distance;
    public bool movingOut;

    private void Start()
    {
        //MoveOut();
    }

    private void Update()
    {
        while (movingOut)
        {
            point1.transform.position = Vector3.Lerp(point1.transform.position, new Vector3(0, distance, 0), Time.deltaTime * speed);
            point2.transform.position = Vector3.Lerp(point2.transform.position, new Vector3(distance, 0, 0), Time.deltaTime * speed);
            point3.transform.position = Vector3.Lerp(point3.transform.position, new Vector3(0, -distance, 0), Time.deltaTime * speed);
            point4.transform.position = Vector3.Lerp(point4.transform.position, new Vector3(-distance, 0, 0), Time.deltaTime * speed);

            if (point1.transform.position.y >= distance)
                movingOut = false;
        }

        while (!movingOut)
        {
            point1.transform.position = Vector3.Lerp(point1.transform.position, new Vector3(0, -distance, 0), Time.deltaTime * speed);
            point2.transform.position = Vector3.Lerp(point2.transform.position, new Vector3(-distance, 0, 0), Time.deltaTime * speed);
            point3.transform.position = Vector3.Lerp(point3.transform.position, new Vector3(0, distance, 0), Time.deltaTime * speed);
            point4.transform.position = Vector3.Lerp(point4.transform.position, new Vector3(distance, 0, 0), Time.deltaTime * speed);

            if (point1.transform.position.y >= 0)
                movingOut = true;
        }
    }
    void MoveOut()
    {

        Debug.Log("moved out");

        for (int i = 0; i < 25; i++)
        {
            point1.transform.position = Vector3.Lerp(point1.transform.position, new Vector3(0, distance, 0), Time.deltaTime * speed);
            point2.transform.position = Vector3.Lerp(point2.transform.position, new Vector3(distance, 0, 0), Time.deltaTime * speed);
            point3.transform.position = Vector3.Lerp(point3.transform.position, new Vector3(0, -distance, 0), Time.deltaTime * speed);
            point4.transform.position = Vector3.Lerp(point4.transform.position, new Vector3(-distance, 0, 0), Time.deltaTime * speed);
    }

    Invoke("MoveIn", delay);
    }

    void MoveIn()
    {

        Debug.Log("moved in");

        for (int i = 0; i < 25; i++)
        {
            point1.transform.position = Vector3.Lerp(point1.transform.position, new Vector3(0, -distance, 0), Time.deltaTime * speed);
            point2.transform.position = Vector3.Lerp(point2.transform.position, new Vector3(-distance, 0, 0), Time.deltaTime * speed);
            point3.transform.position = Vector3.Lerp(point3.transform.position, new Vector3(0, distance, 0), Time.deltaTime * speed);
            point4.transform.position = Vector3.Lerp(point4.transform.position, new Vector3(distance, 0, 0), Time.deltaTime * speed);
        }

        Invoke("MoveOut", delay);
    }


}
