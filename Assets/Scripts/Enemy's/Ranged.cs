using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : EnemyBase
{
    private void Awake()
    {
        health = 2;
        damage = 1;
        speed = .6f;
    }
}
