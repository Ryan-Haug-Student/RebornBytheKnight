using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordGuy : BaseEnemy
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
    }

    void SetStats()
    {
        //calculate difficulty based on current round on a exponential curve
        float difficultyMul = (PC.Instance.currentStage * 0.5f);
        difficultyMul *= difficultyMul; //squared

        health = 10 * difficultyMul;
        armor = 0;

        moveSpeed = 2 * difficultyMul;
        maxDistFromPoint = 1;

        damage = 1 * difficultyMul;
        attackCooldown = 2 - (difficultyMul * .25f);
    }
}
