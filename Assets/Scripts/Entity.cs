using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;

    [Header("Health")]
    public int health;
    public int maxHealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        maxHealth = health;
    }

    protected virtual void Update()
    {
        SpriteHandling();
    }

    private void SpriteHandling()
    {
        if (rb.velocity.x > 0)
            sr.flipX = false;
        else if (rb.velocity.x < 0)
            sr.flipX = true;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
    }

    public virtual void Die() //virtual to be overriden by player to show stats and end run
    {
        print(gameObject + "Died");
        Destroy(gameObject);
    }
}
