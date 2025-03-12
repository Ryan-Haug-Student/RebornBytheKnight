using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image healthFill;

    public Image ACB;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthFill.fillAmount = PlayerController.instance.health / PlayerController.instance.maxHealth;

        if (!PlayerController.instance.canAttack)
            Bar();
        else
            ACB.fillAmount = 0f;
    }

    void Bar()
    {
        ACB.fillAmount = 1f;

    }
}
