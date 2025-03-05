using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : Entity
{
    [Header("Stats")]
    public float health;
    public float moveSpeed;
    public bool canMove;

    public int damage;
    public float attackCooldown;
    public bool canAttack;

    public bool dropsPowerUp;

    public GameObject powerUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerController.instance.hitBox)
            health -= PlayerController.instance.damage;

        if (health <= 0)
        {
            PlayerController.instance.score += Random.Range(1 * PlayerController.instance.stage, 100 * PlayerController.instance.stage);

            if (Random.Range(1, 10) <= PlayerController.instance.stage) // check to drop powerup, current stage out of 10 chance
                PowerUp();
            else
                Destroy(gameObject);

        }
    }

    void PowerUp()
    {
        print("dropped powerup");
        Instantiate(powerUp, transform.position, Quaternion.identity);
    }

    private void DropPowerUp()
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
        Destroy(gameObject);
    }
}
