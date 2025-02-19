using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class PC : MonoBehaviour
{
    public static PC Instance { get; private set; }

    [Header("Stats")]
    public float moveSpeed;

    public float dashStrength; //added to moveSpeed then applied
    public float dashCooldown;

    public float damage;
    public float attackCooldown; //max value of 2, 

    [Header("Bools")]
    public bool canDash = true;
    public bool canAttack = true;

    [Header("Misc")]
    public MoveDirection direction;
    public GameObject hitBox;

    // --private vars & gameObj references--
    private bool isFirstLoad = true;
    private bool isDashing = false;
    private bool isAttacking = false;

    private Rigidbody2D rb;


    // end vars ---------------------------------
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //used to ensure that only 1 player obj persists
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        if (isFirstLoad)
            ResetStats();
    }
    void Update()
    {
        Move();
        if(!isAttacking)
            DirectionControl();

        if (canAttack && Input.GetKey(KeyCode.Space) && direction != MoveDirection.STATIC)
            Attack();
    }

    // set default stats here -------------------
    private void ResetStats()
    {
        isFirstLoad = false;

        //set stats to defaults
        moveSpeed = 3;

        dashStrength = 12;
        dashCooldown = 4;

        damage = 1;
        attackCooldown = 1;
    }

    // begin control functions here -------------

    private void Move()
    {
        //reg movement
        Vector2 futurePos = Vector2.zero;

        futurePos += new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        futurePos *= Time.deltaTime;

        //dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            canDash = false;
            isDashing = true;

            StartCoroutine(EndDash());
        }

        //set velocity
        if (!isDashing)
            rb.velocity = futurePos.normalized * moveSpeed;
        else
            rb.velocity = futurePos.normalized * (moveSpeed + dashStrength);
    }
    private IEnumerator EndDash()
    {
        yield return new WaitForSeconds(.1f);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown - .1f);
        canDash = true; //minus to account for dash duration
    }

    private void Attack()
    {
        canAttack = false;
        isAttacking = true;

        hitBox.SetActive(true);

        StartCoroutine(EndAttack());
    }
    private IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(.1f);
        hitBox.SetActive(false);
        isAttacking = false;

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void DirectionControl()
    {
        if (rb.velocityX == 0 && rb.velocityY == 0)
        {
            direction = MoveDirection.STATIC;
        }

        else if (rb.velocityY > 0) // when moving up
        {
            if (rb.velocityX == 0)
                direction = MoveDirection.UP;
            else if (rb.velocityX > 0)
                direction = MoveDirection.UPRIGHT;
            else
                direction = MoveDirection.UPLEFT;
        }

        else if (rb.velocityY < 0) // when moving down
        {
            if (rb.velocityX == 0)
                direction = MoveDirection.DOWN;
            else if (rb.velocityX > 0)
                direction = MoveDirection.DOWNRIGHT;
            else
                direction = MoveDirection.DOWNLEFT;
        }

        else if (rb.velocityX != 0) //when not moving up or down
        {
            if (rb.velocityX > 0)
                direction = MoveDirection.RIGHT;
            else
                direction = MoveDirection.LEFT;
        }
    }
    public enum MoveDirection
    {
        STATIC,
        UP,
        UPRIGHT,
        RIGHT,
        DOWNRIGHT,
        DOWN,
        DOWNLEFT,
        LEFT,
        UPLEFT
    }
}
