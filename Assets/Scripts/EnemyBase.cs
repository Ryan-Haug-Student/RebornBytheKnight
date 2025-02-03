using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int health;
    public float speed;
    public int damage;

    public GameObject player;
    public GameObject playerSword;
    PlayerController playerController;

    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        playerSword = GameObject.Find("AttackHitBox");
    }
    
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided");
        if (collision.collider.isTrigger)
        {
            health -= (int)playerController.damage;
            Debug.Log("Hit by player");
        }
        DeathCheck();
    }

    private void DeathCheck()
    {
        if (this.health <= 0)
            Destroy(this);
    }
}
