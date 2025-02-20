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
        
    }

    //-------------------------------------------funcs needed, sprite handling, take damage, move, die, reward dropped,

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == PC.Instance.hitBox.name)
        {
            Debug.Log(gameObject.name + " took damage");
            health -= PC.Instance.damage - armor;
        }
    }


}
