using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    public TMP_Text score;
    public TMP_Text stats_txt;


    void Update()
    {
        PlayerController pc = PlayerController.instance;

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

        //stats
        score.text = $"Score: {PlayerController.instance.score}";
        stats_txt.text = 
            $"Health: {pc.health} / {pc.maxHealth} \n" +
            $"Speed: {pc.moveSpeed} \n" +
            $"DashStrength: {pc.dashStrength}, Dash Cooldown: {pc.dashCooldown} \n" +
            $"Damage: {pc.damage}, Attack Cooldown: {pc.attackCooldown} \n" +
            $"Stage: {pc.stage}";


    }

    private IEnumerator AttackBar()
    {
        triggered = true;
        aBar.fillAmount = 1f;

        for (int i = 0; i < 105; i++)
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

        for (int i = 0; i < 102; i++)
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
