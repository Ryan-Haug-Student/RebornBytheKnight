using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordGuy : BaseEnemy
{
    private void Awake()
    {
        //calculate difficulty based on current round on a exponential curve
        float difficultyMul = (PC.Instance.currentStage * 0.25f);
        difficultyMul *= difficultyMul; //squared || when stage = 1, diffculty is .25 -> 2 = 1, 3 = 2.25, 4 = 4, 5 = 6.25

        health = 3 * difficultyMul;
        armor = 0;

        moveSpeed = 2 * difficultyMul;
        maxDistFromPoint = 1;

        damage = 1 * difficultyMul;
        attackCooldown = 2 - (difficultyMul * .25f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
