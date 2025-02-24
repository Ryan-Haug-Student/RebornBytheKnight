using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigTankyDude : BaseEnemy
{
    private void Awake()
    {
        Invoke("SetStats", 0.05f);
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
        float difficultyMul = (PC.Instance.currentStage * 0.5f);
        difficultyMul *= difficultyMul; //squared

        health = 13 * difficultyMul;
        armor = 3 * difficultyMul;

        moveSpeed = 1.5f * difficultyMul;
        maxDistFromPoint = 2;

        damage = 3 * difficultyMul;
        attackCooldown = 3 - (difficultyMul * .25f);
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
