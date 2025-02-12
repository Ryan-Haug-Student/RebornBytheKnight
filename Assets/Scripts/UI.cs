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

    void HealthBar()
    {
        float target = playerController.health / playerController.maxHealth;

        // Update health bar scale
        health.transform.localScale = new Vector3(0.42f * target, 0.08f, 1);

        // Update health bar position
        health.transform.localPosition = new Vector3(-0.21f * (1 - target), -0.02f, 0);
    }
}