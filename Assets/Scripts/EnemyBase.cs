using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float health;
    public float speed;
    public float damage;

    [Header("Gameobject references")]
    public GameObject player;
    public GameObject target;

    protected PlayerController playerController;
    Rigidbody2D rb;

    virtual protected void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();

        rb = gameObject.GetComponent<Rigidbody2D>();

        int tempRandom = Random.Range(1, 5);
        if (tempRandom == 1)
            target = GameObject.Find("P1");
        else if (tempRandom == 2)
            target = GameObject.Find("P2");
        else if (tempRandom == 3)
            target = GameObject.Find("P3");
        else
            target = GameObject.Find("P4");
    }

    protected void Move()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;

        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with:  " + collision.gameObject.name);
        if (collision.gameObject.name == "AttackHitBox")
        {
            health -= playerController.damage;
            Debug.Log("Hit by player");
        }
        DeathCheck();
    }

    private void DeathCheck()
    {
        if (this.health <= 0)
            Destroy(gameObject);
    }
}
