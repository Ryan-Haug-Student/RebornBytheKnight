using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject player;
    public GameObject health;
    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();

        health = GameObject.Find("Health");
    }

    
    void Update()
    {
        HealthBar();
    }


    //full health bar x scale 0.42, x pos 0
    //empty bar is x scale 0, x pos -0.21
    void HealthBar()
    {
        float target = playerController.health / playerController.maxHealth;

        Vector2 full = new Vector2(0.42f, 0); //creating a vector 2 to store bar state, x = scale, y = pos
        Vector2 empty = new Vector2(0, -0.21f);

        float current = full.x * target;
        float current2 = empty.y * target;

        health.transform.localScale.Set(current, 0, 0);
        health.transform.position.Set(0, current2, 0);
    }
}
