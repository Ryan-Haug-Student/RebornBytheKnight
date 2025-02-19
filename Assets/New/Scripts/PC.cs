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

    [Header("Bools")]
    public bool canDash = true;

    [Header("Misc")]
    public MoveDirection direction;

    // --private vars & gameObj references--
    private bool isFirstLoad = true;
    private bool isDashing = false;

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
        DirectionControl();
    }

    // set default stats here -------------------
    private void ResetStats()
    {
        isFirstLoad = false;

        //set stats to defaults
        moveSpeed = 4;

        dashStrength = 6;
        dashCooldown = 3;
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

            StartCoroutine(endDash());
        }

        //set velocity
        if (!isDashing)
            rb.velocity = futurePos.normalized * moveSpeed;
        else
            rb.velocity = futurePos.normalized * (moveSpeed + dashStrength);
    }
    private IEnumerator endDash()
    {
        yield return new WaitForSeconds(.08f);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown - .08f);
        canDash = true; //minus to account for dash duration
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
            else if (rb.velocityX < 0)
                direction = MoveDirection.UPLEFT;
        }

        else if (rb.velocityY < 0) // when moving down
        {
            if (rb.velocityX == 0)
                direction = MoveDirection.DOWN;
            else if (rb.velocityX > 0)
                direction = MoveDirection.DOWNRIGHT;
            else if (rb.velocityX < 0)
                direction = MoveDirection.DOWNLEFT;
        }

        else if (rb.velocityX != 0)
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
