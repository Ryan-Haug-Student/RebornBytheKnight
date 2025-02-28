using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : Entity
{
    public PlayerController PC { get; private set; }

    [Header("Movement")]
    public int moveSpeed = 3;
    public float dashStrength = 6;
    public float dashCooldown = 2;
    public bool canDash = true;
    private bool isDashing;

    [Header("Misc")]
    [SerializeField] MoveDirection moveDirection;
    private GameObject croshair;

    void Start()
    {
        //ensure only one player persists accross scenes
        if (PC == null)
        { PC = this; DontDestroyOnLoad(this); }
        else
            Destroy(gameObject);

        croshair = GameObject.Find("Croshair");
    }

    protected override void Update()
    {
        //run parent entity update function
        base.Update(); 

        Move();
        DirectionControl();
    }

    private void Move()
    {
        Vector2 direction;
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        //direction *= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            StartCoroutine("Dash");

        if (!isDashing)
            rb.velocity = direction * moveSpeed;
        else
            rb.velocity = direction * moveSpeed * dashStrength;

        direction = Vector2.zero;
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        yield return new WaitForSeconds(.1f);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(1);
    }

    private void DirectionControl()
    {
        // Get raw input (-1, 0, or 1 for both axes)
        int h = (int)Input.GetAxisRaw("Horizontal");
        int v = (int)Input.GetAxisRaw("Vertical");

        moveDirection = (h, v) switch
        {
            (0, 1) => MoveDirection.UP,
            (1, 1) => MoveDirection.UPRIGHT,
            (1, 0) => MoveDirection.RIGHT,
            (1, -1) => MoveDirection.DOWNRIGHT,
            (0, -1) => MoveDirection.DOWN,
            (-1, -1) => MoveDirection.DOWNLEFT,
            (-1, 0) => MoveDirection.LEFT,
            (-1, 1) => MoveDirection.UPLEFT,
            _ => MoveDirection.STATIC
        };

        // Update croshair position
        croshair.transform.position = transform.position + new Vector3(h * 0.8f, v * 0.8f, 0).normalized;
    }
    private enum MoveDirection 
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
