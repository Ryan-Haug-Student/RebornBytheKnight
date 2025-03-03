using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHolder : MonoBehaviour
{
    public static GameObject[] commonPowerUps { get; private set; }
    public static GameObject[] uncommonPowerUps { get; private set; }
    public static GameObject[] rarePowerUps { get; private set; }
    public static GameObject[] mythicalPowerUps { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        //load common powerups
        commonPowerUps = Resources.LoadAll<GameObject>("PowerUps/Common");
        print($"Loaded {commonPowerUps.Length} common power-Ups.");

        //load uncommon powerups
        uncommonPowerUps = Resources.LoadAll<GameObject>("PowerUps/Uncommon");
        print($"Loaded {uncommonPowerUps.Length} common power-Ups.");

        //load rare powerups
        rarePowerUps = Resources.LoadAll<GameObject>("PowerUps/Rare");
        print($"Loaded {rarePowerUps.Length} rare power-Ups");

        mythicalPowerUps = Resources.LoadAll<GameObject>("PowerUps/Mythical");
        print($"Loaded {mythicalPowerUps.Length} mythical power-Ups");
    }

}
