using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : BaseEnemy
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

        if (canAttack && distFromPlayer < 1f)
            Attack();
    }

    void SetStats()
    {
        //calculate difficulty based on current round on a exponential curve
        float difficultyMul = (PC.Instance.currentStage * 0.5f);
        difficultyMul *= difficultyMul; //squared

        health = 5 * difficultyMul;
        armor = 0;

        moveSpeed = 4 * difficultyMul;
        maxDistFromPoint = 0;

        damage = .5f * difficultyMul;
        attackCooldown = 1.25f - (difficultyMul * .25f);
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
