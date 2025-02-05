using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject attackIndicator;

    [Header("Player Stats")]
    public float health;
    public float speed;

    public float damage;
    public float attckCooldown;

    [Header("Bools")]
    public bool canAttack;

    [Header("Misc")]
    public MoveDirection moveDirection;
    public GameObject AHB; //AttackHitBox || public so can be accessed by other scripts

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        attackIndicator = GameObject.Find("AttackIndicator");

        AHB.SetActive(false);
    }


    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space) && canAttack && moveDirection != MoveDirection.STATIC)
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
    //MAKE FUNCTION TO LIMIT SPEED AND SET INITIAL FORCE HIGHER FOR QUICKER ACCEL

    private void Attack()
    {
        canAttack = false;

        //manage the attackhitbox rotation while attacking
        if (moveDirection == MoveDirection.UPRIGHT || moveDirection == MoveDirection.DOWNLEFT)
            AHB.transform.rotation = Quaternion.Euler(0, 0, -45);

        else if (moveDirection == MoveDirection.UPLEFT || moveDirection == MoveDirection.DOWNRIGHT)
            AHB.transform.rotation = Quaternion.Euler(0, 0, 45);

        else        //if (moveDirection == MoveDirection.LEFT || moveDirection == MoveDirection.RIGHT)
            AHB.transform.rotation = Quaternion.Euler(0, 0, 90);

        //else
        //    AHB.transform.rotation = Quaternion.Euler(0, 0, 0);

        AHB.SetActive(true);
        Debug.Log("attacked");
        Invoke("RemoveHitbox", attckCooldown * .1f);
        
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
            // use this for when you want a static position <---> deleted to make attacks always have a place to land
            //
            //case Vector2 position when position == new Vector2(0, 0):
            //    moveDirection = MoveDirection.STATIC;
            //    attackIndicator.transform.position = this.transform.position;
            //    break;

            case Vector2 position when position.x == 0 && position.y > 0:
                moveDirection = MoveDirection.UP;
                attackIndicator.transform.position = this.transform.position + new Vector3(0, .8f);
                break;

            case Vector2 position when position.x > 0 && position.y > 0:
                moveDirection = MoveDirection.UPRIGHT;
                attackIndicator.transform.position = this.transform.position + new Vector3(.8f, .8f);
                break;

            case Vector2 position when position.x > 0 && position.y == 0:
                moveDirection = MoveDirection.RIGHT;
                attackIndicator.transform.position = this.transform.position + new Vector3(.8f, 0);
                break;

            case Vector2 position when position.x > 0 && position.y < 0:
                moveDirection = MoveDirection.DOWNRIGHT;
                attackIndicator.transform.position = this.transform.position + new Vector3(.8f, -.8f);
                break;

            case Vector2 position when position.x == 0 && position.y < 0:
                moveDirection = MoveDirection.DOWN;
                attackIndicator.transform.position = this.transform.position + new Vector3(0, -.8f);
                break;

            case Vector2 position when position.x < 0 && position.y < 0:
                moveDirection = MoveDirection.DOWNLEFT;
                attackIndicator.transform.position = this.transform.position + new Vector3(-.8f, -.8f);
                break;

            case Vector2 position when position.x < 0 && position.y == 0:
                moveDirection = MoveDirection.LEFT;
                attackIndicator.transform.position = this.transform.position + new Vector3(-.8f, 0);
                break;

            case Vector2 position when position.x < 0 && position.y > 0:
                moveDirection = MoveDirection.UPLEFT;
                attackIndicator.transform.position = this.transform.position + new Vector3(-.8f, .8f);
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
