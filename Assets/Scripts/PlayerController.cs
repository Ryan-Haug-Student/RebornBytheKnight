using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject attackIndicator;
    GameObject AHB; //AttackHitBox

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

        attackIndicator = GameObject.Find("AttackIndicator");

        //this must be active at start to find object
        AHB = GameObject.Find("AttackHitBox");
        AHB.SetActive(false);
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
            futurePos += new Vector2(Input.GetAxisRaw("Horizontal") * Time.deltaTime * 10, 0);
        
        if (Input.GetAxisRaw("Vertical") != 0)
            futurePos += new Vector2(0, Input.GetAxisRaw("Vertical") * Time.deltaTime * 10);
        

        rb.AddForce(futurePos.normalized * speed);
        DirectionControl(futurePos);

        futurePos = Vector2.zero;
    }

    private void Attack()
    {
        canAttack = false;

        //manage the attackhitbox rotation while attacking
        if (moveDirection == MoveDirection.UPRIGHT || moveDirection == MoveDirection.DOWNLEFT)
            AHB.transform.rotation = Quaternion.Euler(0, 0, -45);
        else if (moveDirection == MoveDirection.UPLEFT || moveDirection == MoveDirection.DOWNRIGHT)
            AHB.transform.rotation = Quaternion.Euler(0, 0, 45);
        else if (moveDirection == MoveDirection.LEFT || moveDirection == MoveDirection.RIGHT)
            AHB.transform.rotation = Quaternion.Euler(0, 0, 90);
        else
            AHB.transform.rotation = Quaternion.Euler(0, 0, 0);

        AHB.SetActive(true);
        Debug.Log("attacked");
        Invoke("RemoveHitbox", attckCooldown * .2f);
        
        Invoke("ResetAttack", attckCooldown);
    }
    private void ResetAttack()
    {
        canAttack = true;
    }
    private void RemoveHitbox()
    {
        AHB.SetActive(false);
    }

    private void DirectionControl(Vector2 futurePos)
    {
        switch (futurePos)
        {
            case Vector2 position when position == new Vector2(0, 0):
                moveDirection = MoveDirection.STATIC;
                attackIndicator.transform.position = this.transform.position;
                break;

            case Vector2 position when position.x == 0 && position.y > 0:
                moveDirection = MoveDirection.UP;
                attackIndicator.transform.position = this.transform.position + new Vector3(0, 1);
                break;

            case Vector2 position when position.x > 0 && position.y > 0:
                moveDirection = MoveDirection.UPRIGHT;
                attackIndicator.transform.position = this.transform.position + new Vector3(1, 1);
                break;

            case Vector2 position when position.x > 0 && position.y == 0:
                moveDirection = MoveDirection.RIGHT;
                attackIndicator.transform.position = this.transform.position + new Vector3(1, 0);
                break;

            case Vector2 position when position.x > 0 && position.y < 0:
                moveDirection = MoveDirection.DOWNRIGHT;
                attackIndicator.transform.position = this.transform.position + new Vector3(1, -1);
                break;

            case Vector2 position when position.x == 0 && position.y < 0:
                moveDirection = MoveDirection.DOWN;
                attackIndicator.transform.position = this.transform.position + new Vector3(0, -1);
                break;

            case Vector2 position when position.x < 0 && position.y < 0:
                moveDirection = MoveDirection.DOWNLEFT;
                attackIndicator.transform.position = this.transform.position + new Vector3(-1, -1);
                break;

            case Vector2 position when position.x < 0 && position.y == 0:
                moveDirection = MoveDirection.LEFT;
                attackIndicator.transform.position = this.transform.position + new Vector3(-1, 0);
                break;

            case Vector2 position when position.x < 0 && position.y > 0:
                moveDirection = MoveDirection.UPLEFT;
                attackIndicator.transform.position = this.transform.position + new Vector3(-1, 1);
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
