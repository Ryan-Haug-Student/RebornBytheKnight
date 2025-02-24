using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [Header("Cost")]
    [SerializeField] GameObject melee; [SerializeField] int meleeCost;
    [SerializeField] GameObject tank; [SerializeField] int tankCost;
    [SerializeField] GameObject goblin; [SerializeField] int goblinCost;

    [SerializeField] GameObject ranged; [SerializeField] int rangedCost;
}
