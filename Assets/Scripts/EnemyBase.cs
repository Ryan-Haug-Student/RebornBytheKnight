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
        //playerSword = GameObject.Find();
    }
    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with:  " + collision.gameObject.name);
        if (collision.gameObject.name == "AttackHitBox")
        {
            health -= (int)playerController.damage;
            Debug.Log("Hit by player");
        }
        DeathCheck();
    }

    private void DeathCheck()
    {
        if (this.health <= 0)
            Destroy(gameObject);
    }
}
