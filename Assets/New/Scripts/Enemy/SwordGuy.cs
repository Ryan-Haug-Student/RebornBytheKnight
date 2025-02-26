using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordGuy : BaseEnemy
{
    private void Awake()
    {
        Invoke("SetStats", 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        SpriteHandler();

        if (canAttack && distFromPlayer < 1.5f)
            Attack();
    }

    void SetStats()
    {
        //calculate difficulty based on current round on a exponential curve
        float difficultyMul = (PC.Instance.currentStage * 0.33f);
        difficultyMul *= difficultyMul; //squared

        health = 10 * difficultyMul;
        armor = 0;

        moveSpeed = 2 * difficultyMul;
        maxDistFromPoint = 1;

        damage = 1 * difficultyMul;
        attackCooldown = 2 - (difficultyMul * .25f);
        canAttack = true;
    }

    void Attack()
    {
        Debug.Log(gameObject + " attacked");

        canAttack = false;
        PC.Instance.health -= damage;

        Invoke("ResetAttack", attackCooldown);
    }
    void ResetAttack()
    {
        canAttack = true;
    }
}
