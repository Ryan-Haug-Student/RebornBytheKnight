using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemy : Enemy
{
    private void Start()
    {
        health = 50;
        moveSpeed = 2.5f;

        damage = 10;
        attackCooldown = 2;
        canAttack = true;
    }
}
