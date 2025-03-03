using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

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
        int rv = Random.Range(1, 101);
        GameObject PTD;// power up to drop

        if (rv <= 50)
        {
            PTD = PowerUpHolder.commonPowerUps[Random.Range(0, PowerUpHolder.commonPowerUps.Length)];
            print("common PU dropped");
        }
        else if (rv <= 75)
        {
            PTD = PowerUpHolder.uncommonPowerUps[Random.Range(0, PowerUpHolder.uncommonPowerUps.Length)];
            print("uncommon PU dropped");
        }
        else if (rv <= 92)
        {
            PTD = PowerUpHolder.rarePowerUps[Random.Range(0, PowerUpHolder.rarePowerUps.Length)];
            print("rare PU dropped");
        }
        else
        {
            PTD = PowerUpHolder.mythicalPowerUps[Random.Range(0, PowerUpHolder.mythicalPowerUps.Length)];
            print("myth PU dropped");
        }

        Instantiate(PTD);
    }
}
