using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasePowerUp : MonoBehaviour
{
    public Rarity rarity;

    private void Update()
    {
        if (Input.GetKey(KeyCode.T))
            ChooseRarity();
    }

    void ChooseRarity()
    {
        int RV = Random.Range(1, 16);       //RV = random value
        if (RV <= 7)                        // 7 out of 15 will be common
            rarity = Rarity.COMMON;         // 4 out of 15 will be uncommon
        else if (RV <= 11)                  // 3 out of 15 will be rare
            rarity = Rarity.UNCOMMON;       // 2 out of 15 will be mythical
        else if (RV <= 14)
            rarity = Rarity.RARE;
        else
            rarity = Rarity.MYTHICAL;

        Debug.Log(rarity);
    }

    public enum Rarity
    {
        COMMON,
        UNCOMMON,
        RARE,
        MYTHICAL
    }

}
