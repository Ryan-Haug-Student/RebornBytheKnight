using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int rarity;
    public int statToAffect;

    private void Awake()
    {
        int temp = Random.Range(0, 100);
        if (temp <= 40)
            rarity = 1;
        else if (temp <= 70)
            rarity = 2;
        else if (temp <= 90)
            rarity = 3;
        else
            rarity = 4;


        statToAffect = Random.Range(1, 8);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerController.instance.gameObject)
        {
            switch (statToAffect)
            {
                case 1: // Affects health
                    if (rarity == 1)
                        PlayerController.instance.health += 25;
                    else if (rarity == 2)
                        PlayerController.instance.health += 40;
                    else if (rarity == 3)
                        PlayerController.instance.health *= 2;
                    else
                    {
                        PlayerController.instance.health *= 2;
                        PlayerController.instance.maxHealth += 50;
                    }
                    break;

                case 2: // Affects max health
                    if (rarity == 1)
                        PlayerController.instance.maxHealth += 10;
                    else if (rarity == 2)
                        PlayerController.instance.maxHealth += 20;
                    else if (rarity == 3)
                        PlayerController.instance.maxHealth += 50;
                    else
                    {
                        PlayerController.instance.health *= 2;
                    }
                    break;

                case 3: // Affects moveSpeed
                    if (rarity == 1)
                        PlayerController.instance.moveSpeed += 0.5f;
                    else if (rarity == 2)
                        PlayerController.instance.moveSpeed += 1;
                    else if (rarity == 3)
                        PlayerController.instance.moveSpeed += 2;
                    else
                        PlayerController.instance.moveSpeed += 3;
                    break;

                case 4: // Affects dashStrength
                    if (rarity == 1)
                        PlayerController.instance.dashStrength += 2;
                    else if (rarity == 2)
                        PlayerController.instance.dashStrength += 3;
                    else if (rarity == 3)
                        PlayerController.instance.dashStrength += 4;
                    else
                        PlayerController.instance.dashStrength += 6;
                    break;

                case 5: // Affects dashCooldown
                    if (rarity == 1)
                        PlayerController.instance.dashCooldown *= 0.95f;
                    else if (rarity == 2)
                        PlayerController.instance.dashCooldown *= 0.8f;
                    else if (rarity == 3)
                        PlayerController.instance.dashCooldown *= 0.7f;
                    else
                        PlayerController.instance.dashCooldown *= 0.55f;
                    break;

                case 6: // Affects canDash
                    if (rarity == 1 || rarity == 2 || rarity == 3 || rarity == 4)
                        PlayerController.instance.canDash = true; // Always enable dashing
                    break;

                case 7: // Affects damage
                    if (rarity == 1)
                        PlayerController.instance.damage += 3;
                    else if (rarity == 2)
                        PlayerController.instance.damage += 5;
                    else if (rarity == 3)
                        PlayerController.instance.damage += 10;
                    else
                        PlayerController.instance.damage += 15;
                    break;

                case 8: // Affects attackCooldown
                    if (rarity == 1)
                        PlayerController.instance.attackCooldown *= 0.9f;
                    else if (rarity == 2)
                        PlayerController.instance.attackCooldown *= 0.85f;
                    else if (rarity == 3)   
                        PlayerController.instance.attackCooldown *= 0.65f;
                    else
                        PlayerController.instance.attackCooldown *= .5f;
                    break;

                default:
                    Debug.LogWarning("Invalid statToAffect value.");
                    break;
            }

            Destroy(gameObject);
        }
    }
}
