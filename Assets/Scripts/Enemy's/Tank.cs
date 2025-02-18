using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Melee
{
    new void Awake()
    {
        health = 5;
        damage = 2;
        speed = .6f;

        canAttack = true;
        attackDistance = .8f;

        // Set up the layer mask to ignore the "Enemy" layer (layer 6)
        layerMask = ~(1 << 6); // ~ means "ignore this layer"
    }
}
