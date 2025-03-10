using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageEnemy : Enemy
{
    public GameObject zone;

    void Start()
    {
        health = 40;
        //damage is handled in the zone script
        attackCooldown = 8;
        canAttack = true;

        canMove = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (canAttack)
            StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        canAttack = false;

        float rndX = Random.Range(-7, 7);
        float rndY = Random.Range(-3, 3);

        Vector2 spawnPos = new Vector2(rndX, rndY);

        Instantiate(zone, spawnPos, Quaternion.identity);

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
