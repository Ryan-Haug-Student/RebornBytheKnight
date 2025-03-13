using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image healthFill;

    public Image aBar;
    public Image dBar;

    public GameObject stats;

    public bool paused;
    public GameObject pauseMenu;

    private bool triggered;
    private bool triggered2;


    void Update()
    {
        //health bar
        healthFill.fillAmount = PlayerController.instance.health / PlayerController.instance.maxHealth;

        // manage cooldown bars here
        if (!PlayerController.instance.canAttack && !triggered)
            StartCoroutine(AttackBar());
        if (!PlayerController.instance.canDash && !triggered2)
            StartCoroutine(DashBar());

        //stats panel
        if (Input.GetKey(KeyCode.Tab))
                stats.SetActive(true);
        else
            stats.SetActive(false);

        //pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
            CheckForPause();
    }

    private IEnumerator AttackBar()
    {
        triggered = true;
        aBar.fillAmount = 1f;

        for (int i = 0; i < 100; i++)
        {
            aBar.fillAmount -= 0.01f;
            yield return new WaitForSeconds(PlayerController.instance.attackCooldown / 100);
        }

        triggered = false;
    }

    private IEnumerator DashBar()
    {
        triggered2 = true;
        dBar.fillAmount = 1f;

        for (int i = 0; i < 100; i++)
        {
            dBar.fillAmount -= 0.01f;
            yield return new WaitForSeconds(PlayerController.instance.dashCooldown / 100);
        }

        triggered2 = false;
    }

    public void CheckForPause()
    {
        if (!paused)
        {
            paused = true;

            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            paused = false;

            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Quit()
    {
        Destroy(PlayerController.instance.gameObject);

        SceneManager.LoadScene("MainMenu");
    }

}
