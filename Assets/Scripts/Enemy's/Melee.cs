using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : EnemyBase
{
    private bool canAttack;
    private float attackDistance;
    private void Awake()
    {
        health = 3;
        damage = 1;
        speed = .8f;

        canAttack = true;
        attackDistance = .5f;
    }

    private void Update()
    {
        Move();

        if (canAttack)
        {
            canAttack = false;

            Debug.DrawRay(gameObject.transform.position, player.transform.position - gameObject.transform.position, Color.white, attackDistance);

            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, player.transform.position - gameObject.transform.position, attackDistance);
            
            if (hit.collider.gameObject.name == "player")
            {
                playerController.health -= damage;
            }

            Invoke("ResetAttack", 1);
        }
    }
    private void ResetAttack()
    {
        canAttack = true;
    }
}
