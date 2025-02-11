using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : EnemyBase
{
    private bool canAttack;
    private float attackDistance;
    private int layerMask;

    private void Awake()
    {
        health = 3;
        damage = 1;
        speed = .8f;

        canAttack = true;
        attackDistance = .5f;

        // Set up the layer mask to ignore the "Enemy" layer (layer 6)
        layerMask = ~(1 << 6); // ~ means "ignore this layer"
    }

    private void Update()
    {
        Move();

        if (canAttack)
        {
            canAttack = false;

            // Calculate the direction to the player
            Vector2 direction = (player.transform.position - transform.position).normalized;

            // Debug draw the ray (visible in Scene view)
            Debug.DrawRay(transform.position, direction * attackDistance, Color.red, 0.1f);

            RaycastHit2D hit = Physics2D.Raycast(
                transform.position, // Origin of the ray
                direction,          // Direction of the ray
                attackDistance,     // Distance of the ray
                layerMask           // Layer mask to ignore the Brawler
            );

            if (hit.collider != null)
            { 
                if (hit.collider.CompareTag("Player"))
                {
                    playerController.health -= damage;
                    Debug.Log("enemy hit player");
                }
            }
            else
            {
                Debug.Log("Raycast hit nothing.");
            }

            Invoke("ResetAttack", 1);
        }
    }

    private void ResetAttack()
    {
        canAttack = true;
    }
}