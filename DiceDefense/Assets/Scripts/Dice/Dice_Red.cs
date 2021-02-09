﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Red : Dice
{
    public override void Attack(Enemy e)
    {
        bool cri = false;

        int randomCritical = Random.Range(1, 101);
        if (randomCritical <= critical)
            cri = true;

        if (cri)
            e.Hit((damage + UpgradeManager.instance.redDiceUpgrade) * 2);
        else
            e.Hit(damage + UpgradeManager.instance.redDiceUpgrade);
    }

    public override void SkillAttack(Enemy e)
    {
        int layerMask = 1 << LayerMask.NameToLayer("Enemy");
        Collider2D[] enemys = Physics2D.OverlapCircleAll(e.transform.position, 1f, layerMask);

        int len = enemys.Length;

        for (int i = 0; i < len; i++)
        {
            enemys[i].GetComponent<Enemy>().Hit(damage + UpgradeManager.instance.redDiceUpgrade);
        }
    }
}
