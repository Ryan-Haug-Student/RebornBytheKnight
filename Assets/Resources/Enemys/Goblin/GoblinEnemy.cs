using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinEnemy : Enemy
{
    public float attackDistance;
    private LineRenderer lineRenderer; // Reference to the LineRenderer component

    private void Start()
    {
        health = 20;
        moveSpeed = 2.3f;
        canMove = true;

        damage = 5;
        attackCooldown = 1;
        canAttack = true;

        attackDistance = 1f;

        // Get the LineRenderer component attached to this GameObject
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    protected override void Update()
    {
        base.Update();

        Move();
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < attackDistance && canAttack)
            StartCoroutine("Attack");

        // Update the line renderer to always face the player
        UpdateAttackLine();
    }

    private void Move()
    {
        Vector2 direction = (PlayerController.instance.transform.position - transform.position).normalized;

        rb.velocity = direction * moveSpeed;
    }

    private IEnumerator Attack()
    {
        //do the attack and indicate via red line
        canAttack = false;
        PlayerController.instance.health -= damage;
        lineRenderer.startColor = Color.red; lineRenderer.endColor = Color.red;

        //make line grey after indicating attack to indicate cooldown
        yield return new WaitForSeconds(0.05f);
        lineRenderer.startColor = Color.grey; lineRenderer.endColor = Color.grey;
        
        //reset
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        lineRenderer.startColor = Color.white; lineRenderer.endColor = Color.white;
    }

    private void UpdateAttackLine()
    {
        Vector3 direction = (PlayerController.instance.transform.position - transform.position).normalized;

        // Set the start position of the line at enemy
        lineRenderer.SetPosition(0, transform.position);

        // Set the end position of the line looking at player, attackdistance long, -.2 to account for reaching middle of player
        lineRenderer.SetPosition(1, transform.position + direction * (attackDistance - .2f));
    }
}