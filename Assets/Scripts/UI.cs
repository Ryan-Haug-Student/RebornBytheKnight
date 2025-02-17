using UnityEngine;

public class UI : MonoBehaviour
{
    GameObject player;
    GameObject health;

    GameObject InGameElements;
    [SerializeField] GameObject pauseMenu;

    public bool paused;

    void Start()
    {
        player = GameObject.Find("Player");

        health = GameObject.Find("Health");
        InGameElements = GameObject.Find("InGameElements");
    }

    void Update()
    {
        HealthBar(); //update the health bar pos

        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();
    }

    void HealthBar()
    {
        float target = PlayerController.health / PlayerController.maxHealth;

        // Update health bar scale
        health.transform.localScale = new Vector3(0.42f * target, 0.08f, 1);

        // Update health bar position
        health.transform.localPosition = new Vector3(-0.21f * (1 - target), -0.02f, 0);
    }

    public void Pause()
    {
        if (!paused)
        {
            InGameElements.SetActive(false);
            pauseMenu.SetActive(true);

            Time.timeScale = 0;
            paused = true;

            Debug.Log("Paused");
        }
        else
        {
            InGameElements.SetActive(true);
            pauseMenu.SetActive(false);

            Time.timeScale = 1;
            paused = false;

            Debug.Log("unPaused");
        }
    }
}