using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject attackIndicator;
    GameObject AAI; //attack after image
    SpriteRenderer attackSprite;

    [Header("Player Stats")]
    public float health;
    public float speed;
    public float dashStrength;
    public float dashCooldown;

    public float damage;
    public float attckCooldown;

    [Header("Bools")]
    public bool canAttack;
    public bool canDash;

    [Header("Misc")]
    public MoveDirection moveDirection;
    public GameObject AHB; //AttackHitBox || public to be accessed by other scripts

    void Start()
    {
        attackIndicator = GameObject.Find("AttackIndicator");
        AAI = GameObject.Find("AttackAfterImage");

        rb = GetComponent<Rigidbody2D>();
        attackSprite = AAI.GetComponent<SpriteRenderer>();

        AHB.SetActive(false); //hide the hitbox and make it so enemys wont take dmg unless player attacks
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
            futurePos += new Vector2((Input.GetAxisRaw("Horizontal") * 10) * Time.deltaTime, 0);
        
        if (Input.GetAxisRaw("Vertical") != 0)
            futurePos += new Vector2(0, (Input.GetAxisRaw("Vertical") * 10) * Time.deltaTime);
        
        //dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            canDash = false;

            transform.Translate(futurePos.normalized * dashStrength * .25f);
            Invoke("ResetDash", dashCooldown);
        }

        
        rb.velocity = (futurePos.normalized * speed);    
        DirectionControl(futurePos);

        futurePos = Vector2.zero;
    }
    private void ResetDash()
    {
        canDash = true;
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

        //bc sprite doesnt follow the actual direction needed this flips the after image attack sprite as needed
        if(moveDirection == MoveDirection.RIGHT || moveDirection == MoveDirection.DOWNRIGHT || moveDirection == MoveDirection.DOWN || moveDirection == MoveDirection.DOWNLEFT)
            attackSprite.flipY = true;
        else
            attackSprite.flipY = false;

        AHB.SetActive(true);
        Debug.Log("attacked");
        Invoke("RemoveHitbox", attckCooldown * .08f);
        
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
