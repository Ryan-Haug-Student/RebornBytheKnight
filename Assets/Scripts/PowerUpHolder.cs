using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHolder : MonoBehaviour
{
    public static GameObject[] powerUps { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        powerUps = Resources.LoadAll<GameObject>("PowerUps");
        print($"Loaded {powerUps.Length} power-Ups.");
    }
}
