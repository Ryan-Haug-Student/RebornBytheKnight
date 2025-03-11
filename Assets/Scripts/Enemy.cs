using System.Collections;
using UnityEngine;

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
        { health -= PlayerController.instance.damage; StartCoroutine(Knockback()); }

        if (health <= 0)
        {
            PlayerController.instance.score += Random.Range(1 * PlayerController.instance.stage, 100 * PlayerController.instance.stage);

            if (Random.Range(1, 10) <=  1 + (PlayerController.instance.stage / 1.75)) // check to drop powerup, current stage out of 10 chance
                PowerUp();
            else
                Destroy(gameObject);

        }
    }

    void PowerUp()
    {
        print("dropped powerup");
        Instantiate(powerUp, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    protected IEnumerator Knockback()
    {
        canMove = false;
        rb.velocity = (PlayerController.instance.transform.position - transform.position).normalized * -5;

        yield return new WaitForSeconds(0.1f);
        canMove = true;
    }
}
