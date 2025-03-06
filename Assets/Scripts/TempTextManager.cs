using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TempTextManager : MonoBehaviour
{
    public TMP_Text tmp;
    PlayerController pc;
    bool assigned;
    private void Update()
    {
        if (!assigned)
        { pc = PlayerController.instance; assigned = true; }

        tmp.text = $"Health and max health = {pc.health}, {pc.maxHealth} || " +
            $"Movespeed = {pc.moveSpeed} || dash strength and cooldown = {pc.dashStrength}, {pc.dashCooldown}" +
            $" Damage = {pc.damage} || attack cooldown = {pc.attackCooldown}" +
            $" score = {pc.score} || stage = {pc.stage} || " +
            $"canTeleport = {pc.stageOver}";
    }
}
