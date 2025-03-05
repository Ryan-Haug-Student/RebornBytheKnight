using System.Collections;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject arrow;
    public float arrowSpeed = 7f;

    void Start()
    {
        health = 30;
        moveSpeed = 0.8f;
        canMove = true;
        attackCooldown = 2.5f;
        canAttack = true;
    }

    protected override void Update()
    {
        base.Update();

        // Check if the player is within attack range and the enemy can attack
        if (canAttack)
            StartCoroutine(Attack());

        Move();
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        Vector2 direction = (PlayerController.instance.transform.position - transform.position).normalized;

        // Calculate the angle in degrees for 2D rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Instantiate the arrow and set its rotation using fancy math i deffinitely did myself
        GameObject arrowInstance = Instantiate(arrow, transform.position + (Vector3)direction, Quaternion.Euler(0, 0, angle));

        // Set the arrow's velocity
        Rigidbody2D arrowRb = arrowInstance.GetComponent<Rigidbody2D>();
        if (arrowRb != null)
            arrowRb.velocity = direction * arrowSpeed;

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void Move()
    {
        Vector2 direction = (PlayerController.instance.transform.position - transform.position).normalized;

        if (Vector3.Distance(PlayerController.instance.transform.position, transform.position) > 5)
            rb.velocity = direction * moveSpeed;
        else
            rb.velocity = direction * -moveSpeed;
    }
}