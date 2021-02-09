using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Yellow : Dice
{
    public override void Attack(Enemy e)
    {
        bool cri = false;

        int randomCritical = Random.Range(1, 101);
        if (randomCritical <= critical)
            cri = true;

        if (cri)
            e.Hit((damage + UpgradeManager.instance.yellowDiceUpgrade) * 2);
        else
            e.Hit(damage + UpgradeManager.instance.yellowDiceUpgrade);
    }

    public override void SkillAttack(Enemy e)
    {

    }
}
