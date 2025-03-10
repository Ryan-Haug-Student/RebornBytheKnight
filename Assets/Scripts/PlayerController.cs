using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : Entity
{
    public static PlayerController instance { get; private set; }

    [Header("health")]
    public int health = 100;
    public int maxHealth = 100;

    [Header("Movement")]
    public float moveSpeed = 3;
    public float dashStrength = 10;
    public float dashCooldown = 2;
    public bool canDash = true;
    private bool isDashing;

    [Header("Attack")]
    public int damage = 10;
    public float attackCooldown = 2;
    public bool canAttack = true;

    [Header("Game Stats")]
    public int score;
    public int stage = 1;
    public bool stageOver = false;

    [Header("Misc")]
    [SerializeField] MoveDirection moveDirection;
    [SerializeField] public GameObject hitBox;
    private GameObject croshair;
    public Vector2 direction;

    void Start()
    {
        //ensure only one player persists accross scenes
        if (instance == null)
        { instance = this; DontDestroyOnLoad(this); }
        else
            Destroy(gameObject);

        croshair = GameObject.Find("Croshair");

        hitBox.SetActive(false);
    }

    protected override void Update()
    {
        //run parent entity update function
        base.Update(); 

        Move();
        if (hitBox.activeSelf == false)
            DirectionControl();

        if (Input.GetKeyDown(KeyCode.Space) && canAttack) 
            StartCoroutine(Attack());

        if (health <= 0)
            Die();
    }

    private void Move()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        //direction *= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            StartCoroutine("Dash");

        if (!isDashing)
            rb.velocity = direction * moveSpeed;
        else
            rb.velocity = direction * (moveSpeed + dashStrength);

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
        canAttack = false;
        hitBox.SetActive(true);
        
        yield return new WaitForSeconds(.1f);
        hitBox.SetActive(false);

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void Die()
    {
        Time.timeScale = 0;

        print("player died");

        SceneManager.LoadScene("MainMenu");
        Destroy(gameObject);
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
        if (moveDirection != MoveDirection.STATIC)
        {
            croshair.transform.position = transform.position + new Vector3(h * 0.8f, v * 0.8f, 0).normalized;
            hitBox.transform.rotation = Quaternion.Euler(0, 0, ((int)moveDirection - 1) * 45);
        }
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
