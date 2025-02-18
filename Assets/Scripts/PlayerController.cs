using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject attackIndicator;
    GameObject AAI; //attack after image
    SpriteRenderer attackSprite;

    [Header("Player Stats")]
    public float maxHealth;
    public float health;
    public float speed;
    public float dashStrength;
    public float dashCooldown;
            
    public float damage;
    public float attckCooldown;

    [Header("Bools")]
    public bool canAttack;
    public bool canDash;
    bool isDashing;
    bool isFirstLoad = true;

    [Header("Misc")]
    public MoveDirection moveDirection;
    public GameObject AHB; //AttackHitBox || public to be accessed by other scripts

    public static PlayerController Instance { get; private set; }

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }

        if (isFirstLoad)
            SetStats();

        attackIndicator = GameObject.Find("AttackIndicator");
        AAI = GameObject.Find("AttackAfterImage");

        rb = GetComponent<Rigidbody2D>();
        attackSprite = AAI.GetComponent<SpriteRenderer>();

        AHB.SetActive(false); //hide the hitbox and make it so enemys wont take dmg unless player attacks

        maxHealth = health;
    }

    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space) && canAttack && moveDirection != MoveDirection.STATIC)
            Attack();

        if(Input.GetKey(KeyCode.N)) { SceneManager.LoadScene("Stage2"); }
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
            canDash = false; isDashing = true;

            rb.velocity = futurePos * dashStrength * 100;
            Invoke("ResetDash", dashCooldown);
            Invoke("StopDash", dashCooldown * .1f);
        }

        if (!isDashing)
            rb.velocity = futurePos.normalized * speed;

        //stops the attack animation from changing direction mid animation
        if (AHB.activeSelf == false)
            DirectionControl(futurePos);

        futurePos = Vector2.zero;
    }
    private void ResetDash()
    {
        canDash = true;
    }
    private void StopDash()
    {
        isDashing = false;
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
        Invoke("RemoveHitbox", attckCooldown * .12f);
        
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

    private void SetStats()
    {
        isFirstLoad = false;

        maxHealth = 5;
        health = 5;
        speed = 1.8f;
        dashStrength = 5;
        dashCooldown = 2;
        damage = 1;
        attckCooldown = 0.8f;
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