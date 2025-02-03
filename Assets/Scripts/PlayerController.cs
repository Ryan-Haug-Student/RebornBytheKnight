using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    //public stats
    [Header("Player Stats")]
    public float Health;
    public float speed;

    public float damage;
    public float attckCooldown;

    [Header("Bools")]
    public bool canAttack;

    [Header("Misc")]
    public MoveDirection moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
            Attack();
    }

    private void Move()
    {
        Vector2 futurePos = Vector2.zero;

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            futurePos += new Vector2(Input.GetAxisRaw("Horizontal") * Time.deltaTime, 0);
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            futurePos += new Vector2(0, Input.GetAxisRaw("Vertical") * Time.deltaTime);
        }

        rb.AddForce(futurePos.normalized * speed);

        futurePos = Vector2.zero;
    }

    private void Attack()
    {
        canAttack = false;
        Debug.Log("attacked");
        Invoke("ResetAttack", attckCooldown);
    }
    private void ResetAttack()
    {
        canAttack = true;
    }

    private void DirectionControl(Vector2 futurePos)
    {
        switch (futurePos)
        {
            case Vector2 position when position == new Vector2(0, 0):
                moveDirection = MoveDirection.UP;
                Debug.Log("STATIC");
                break;

            case Vector2 position when position.x == 0 && position.y > 0:
                moveDirection = MoveDirection.UP;
                Debug.Log("UP");
                break;

            case Vector2 position when position.x > 0 && position.y > 0:
                moveDirection = MoveDirection.UPRIGHT;
                Debug.Log("UPRIGHT");
                break;

            case Vector2 position when position.x > 0 && position.y == 0:
                moveDirection = MoveDirection.RIGHT;
                Debug.Log("RIGHT");
                break;

            case Vector2 position when position.x > 0 && position.y < 0:
                moveDirection = MoveDirection.DOWNRIGHT;
                Debug.Log("DOWNRIGHT");
                break;

            case Vector2 position when position.x == 0 && position.y < 0:
                moveDirection = MoveDirection.DOWN;
                Debug.Log("DOWN");
                break;

            case Vector2 position when position.x < 0 && position.y < 0:
                moveDirection = MoveDirection.DOWNLEFT;
                Debug.Log("DOWNLEFT");
                break;

            case Vector2 position when position.x < 0 && position.y == 0:
                moveDirection = MoveDirection.LEFT;
                Debug.Log("LEFT");
                break;

            case Vector2 position when position.x < 0 && position.y > 0:
                moveDirection = MoveDirection.UPLEFT;
                Debug.Log("UPLEFT");
                break;
        }
    }

    public enum MoveDirection
        {
        UP,
        UPRIGHT,
        RIGHT,
        DOWNRIGHT,
        DOWN,
        DOWNLEFT,
        LEFT,
        UPLEFT,
        STATIC
    }
}
