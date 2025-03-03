using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Other Stats")]
    public float moveSpeed;
    public int damage;
    public float attackCooldown;
    public bool canAttack;

    protected override void Update()
    {
        base.Update();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == PlayerController.instance.gameObject)
        { TakeDamage(PlayerController.instance.damage); }
    }

    private void damagePlayer()
    {
        PlayerController.instance.health -= damage;
    }

    private void dropPowerUp()
    {
        int rv = Random.Range(1, 11); // rv means rarity value
    }
}
