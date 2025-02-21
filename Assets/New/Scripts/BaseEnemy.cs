using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [Header("stats")]
    public float health;
    public float armor;

    public GameObject pathingPoint;
    public float moveSpeed;
    public float maxDistFromPoint;

    public float damage;
    public float attackCooldown;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    GameObject player;

    void Start()
    {
        int temp = Random.Range(1, 5);
        pathingPoint = GameObject.Find("Point" +  temp);

        rb = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        SpriteHandler();
        Move();
    }

    protected void SpriteHandler()
    {
        if (rb.velocityX > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (rb.velocityX < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == PC.Instance.hitBox.name)
        {
            Debug.Log(gameObject.name + " took damage");
            health -= PC.Instance.damage - armor;

            if (this.health <= 0)
                Destroy(gameObject);
        }
    }

    protected void Move()
    {
        float distFromPlayer = Vector3.Distance(gameObject.transform.position, pathingPoint.transform.position);

        if (distFromPlayer > maxDistFromPoint)
        {
            Vector2 direction = (pathingPoint.transform.position - gameObject.transform.position) * Time.deltaTime;

            rb.velocity = direction.normalized * moveSpeed;
        }
    }

    protected void DropReward()
    {
        //create random number
        //check if random number is less than a certian number, the higher the number the greater the chance, multiply by (current stage * .5)
        //if number is smaller than chance
        //create new random to choose which power up to drop
    }
}
