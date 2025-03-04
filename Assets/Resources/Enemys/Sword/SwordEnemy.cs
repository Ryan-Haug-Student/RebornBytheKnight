using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemy : Enemy
{
    public float attackDistance;
    private LineRenderer lineRenderer; // Reference to the LineRenderer component

    private void Start()
    {
        health = 30;
        moveSpeed = 1.5f;
        canMove = true;

        damage = 10;
        attackCooldown = 2;
        canAttack = true;

        attackDistance = 1.4f;

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

        if (canMove)
        {
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private IEnumerator Attack()
    {
        //start attack warning
        canMove = false;
        canAttack = false;
        lineRenderer.startColor = Color.red; lineRenderer.endColor = Color.red;

        //attack after a quater of a second
        yield return new WaitForSeconds(.25f);
        lineRenderer.startColor = Color.green; lineRenderer.endColor = Color.green;
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < attackDistance)
            PlayerController.instance.health -= damage;

        //start attack cooldown
        yield return new WaitForSeconds(.05f);
        lineRenderer.startColor = Color.grey; lineRenderer.endColor = Color.grey;
        canMove = true;

        //ready to attack
        yield return new WaitForSeconds(attackCooldown);
        lineRenderer.startColor = Color.white; lineRenderer.endColor = Color.white;
        canAttack = true;
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