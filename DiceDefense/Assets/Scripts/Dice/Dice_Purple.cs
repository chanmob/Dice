using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Purple : Dice
{
    public override void Attack(Enemy e)
    {
        bool cri = false;

        int randomCritical = Random.Range(1, 101);
        if (randomCritical <= critical)
            cri = true;

        if (cri)
            e.Hit((damage + UpgradeManager.instance.purpleDiceUpgrade) * 2);
        else
            e.Hit(damage + UpgradeManager.instance.purpleDiceUpgrade);
    }

    public override void SkillAttack(Enemy e)
    {

    }
}
