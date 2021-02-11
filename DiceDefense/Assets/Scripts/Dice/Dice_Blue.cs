using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Blue : Dice
{
    private const float SlowDown = 0.1f;
    private const float SlowDownPerLv = 0.1f;
    private const float SlowDownTime = 2f;

    public override void Attack(Enemy e)
    {
        bool cri = false;

        int randomCritical = Random.Range(1, 101);
        if (randomCritical <= critical)
            cri = true;

        if (cri)
            e.Hit((damage + UpgradeManager.instance.blueDiceUpgrade) * 2);
        else
            e.Hit(damage + UpgradeManager.instance.blueDiceUpgrade);
    }

    public override void SkillAttack(Enemy e)
    {
        e.Slow(SlowDown, SlowDownTime);
        e.Hit(damage + UpgradeManager.instance.blueDiceUpgrade);
    }
}
