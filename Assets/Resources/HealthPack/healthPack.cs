using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPack : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == PlayerController.instance.gameObject)
        {
            if (PlayerController.instance.health <= PlayerController.instance.maxHealth - 25)
                PlayerController.instance.health += 25;
            else
                PlayerController.instance.health = PlayerController.instance.maxHealth;
            
            Destroy(gameObject); 
        }
    }
}
