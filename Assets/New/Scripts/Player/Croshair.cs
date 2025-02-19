using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Croshair : MonoBehaviour
{
    [SerializeField] GameObject hitBox;

    void Update()
    {
        Vector3 direction = Vector3.zero;

        if (PC.Instance.direction == PC.MoveDirection.STATIC)
        {
            direction = Vector3.zero;
            hitBox.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (PC.Instance.direction == PC.MoveDirection.UP)
        {
            direction = Vector3.up;
            hitBox.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (PC.Instance.direction == PC.MoveDirection.UPRIGHT)
        {
            direction = (Vector3.up + Vector3.right).normalized;
            hitBox.transform.rotation = Quaternion.Euler(0, 0, -45);
        }
        else if (PC.Instance.direction == PC.MoveDirection.RIGHT)
        {
            direction = Vector3.right;
            hitBox.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (PC.Instance.direction == PC.MoveDirection.DOWNRIGHT)
        {
            direction = (Vector3.down + Vector3.right).normalized;
            hitBox.transform.rotation = Quaternion.Euler(0, 0, -135);
        }
        else if (PC.Instance.direction == PC.MoveDirection.DOWN)
        {
            direction = Vector3.down;
            hitBox.transform.rotation = Quaternion.Euler(0, 0, -180);
        }
        else if (PC.Instance.direction == PC.MoveDirection.DOWNLEFT)
        {
            direction = (Vector3.down + Vector3.left).normalized;
            hitBox.transform.rotation = Quaternion.Euler(0, 0, -225);
        }
        else if (PC.Instance.direction == PC.MoveDirection.LEFT)
        {
            direction = Vector3.left;
            hitBox.transform.rotation = Quaternion.Euler(0, 0, -270);
        }
        else
        {
            direction = (Vector3.up + Vector3.left).normalized;
            hitBox.transform.rotation = Quaternion.Euler(0, 0, -315);
        }

        // Set the crosshair position at the fixed distance from the player
        gameObject.transform.position = PC.Instance.transform.position + direction * 1.2f;
    }
}