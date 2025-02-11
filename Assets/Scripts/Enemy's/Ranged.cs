using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : EnemyBase
{
    private bool canAttack;
    private int layerMask;
    private Vector2 direction;
    private float delay;

    private void Awake()
    {
        health = 2;
        damage = 1;
        speed = 0;
        delay = 2;

        canAttack = true;       //layermasks are represented by bitmasks so << means shift the 1 bit over 6 positions to represent enemy layer
        layerMask = ~(1 << 6);  //the ~ is the bitwise not operator, telling the raycast to ignore everything BUT this
    }

    private void Update()
    {
        if (canAttack)
        {
            canAttack = false;

            direction = (player.transform.position - transform.position);
            Debug.Log("Ranged Decided on location");

            // Debug draw the ray (visible in Scene view)
            Debug.DrawRay(transform.position, direction, Color.red, 0.2f);

            Invoke("Attack", 1f); //make shoot after 1 second after aiming
        }
    }

    private void Attack()
    {
        Debug.Log("Ranged attacked");

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position, // Origin of the ray
            direction,          // where to shoot
            100,                // distance
            layerMask           // Layer mask to ignore the Brawler
        );

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            playerController.health -= damage;
        }

        Invoke("ResetAttack", delay);
    }
    private void ResetAttack()
    {
        canAttack = true;
    }
}
