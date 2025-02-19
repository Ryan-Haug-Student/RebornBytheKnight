using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Croshair : MonoBehaviour
{
    void Update()
    {
        if (PC.Instance.direction == PC.MoveDirection.STATIC)
        {
            gameObject.transform.position = PC.Instance.transform.position;
        }
        else if (PC.Instance.direction == PC.MoveDirection.UP)
        {
            gameObject.transform.position = (Vector3.up.normalized) + PC.Instance.transform.position;
        }
        else if (PC.Instance.direction == PC.MoveDirection.UPRIGHT)
        {
            gameObject.transform.position = ((Vector3.up + Vector3.right).normalized) + PC.Instance.transform.position;
        }
        else if (PC.Instance.direction == PC.MoveDirection.RIGHT)
        {
            gameObject.transform.position = (Vector3.right.normalized) + PC.Instance.transform.position;
        }
        else if (PC.Instance.direction == PC.MoveDirection.DOWNRIGHT)
        {
            gameObject.transform.position = ((Vector3.down + Vector3.right).normalized) + PC.Instance.transform.position;
        }
        else if (PC.Instance.direction == PC.MoveDirection.DOWN)
        {
            gameObject.transform.position = (Vector3.down.normalized) + PC.Instance.transform.position;
        }
        else if (PC.Instance.direction == PC.MoveDirection.DOWNLEFT)
        {
            gameObject.transform.position = ((Vector3.down + Vector3.left).normalized) + PC.Instance.transform.position;
        }
        else if (PC.Instance.direction == PC.MoveDirection.LEFT)
        {
            gameObject.transform.position = (Vector3.left.normalized) + PC.Instance.transform.position;
        }
        else
        {
            gameObject.transform.position = ((Vector3.up + Vector3.left).normalized) + PC.Instance.transform.position;
        }
    }
}
