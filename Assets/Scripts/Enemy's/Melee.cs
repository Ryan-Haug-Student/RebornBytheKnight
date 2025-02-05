using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : EnemyBase
{
    private void Awake() //setting stats
    {
        health = 3;
        damage = 1;
        speed = 0.8f;
    }

    private void OnCollisionEnter2D(Collision2D collision) //damaging player
    {
        
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("damaged player");
            playerController.health -= damage;
        }
    }
}
