using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image healthFill;

    public Image aBar;
    public Image dBar;

    private bool triggered;
    private bool triggered2;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthFill.fillAmount = PlayerController.instance.health / PlayerController.instance.maxHealth;

        if (!PlayerController.instance.canAttack && !triggered)
            StartCoroutine(AttackBar());
        if (!PlayerController.instance.canDash && !triggered2)
            StartCoroutine(DashBar());

    }

    IEnumerator AttackBar()
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

    IEnumerator DashBar()
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
}
