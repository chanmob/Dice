using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Blue : Dice
{
    private const float SlowDown = 0.1f;
    private const float SlowDownPerLv = 0.1f;
    private const float SlowDownTime = 2f;

    public override void Attack(Enemy e, Bullet b)
    {
        bool cri = false;

        int randomCritical = Random.Range(1, 101);
        if (randomCritical <= critical)
            cri = true;

        if (cri)
        {
            e.Hit((damage + UpgradeManager.instance.blueDiceUpgrade) * 2);

            ParticleDisable p = ObjectPoolManager.instance.GetHitParticle(true);
            p.transform.position = e.transform.position;
            p.gameObject.SetActive(true);
        }
        else
        {
            e.Hit(damage + UpgradeManager.instance.blueDiceUpgrade);

            ParticleDisable p = ObjectPoolManager.instance.GetHitParticle(false);
            p.transform.position = e.transform.position;
            p.gameObject.SetActive(true);
        }
    }

    public override void SkillAttack(Enemy e, Bullet b)
    {
        e.Slow(SlowDown, SlowDownTime);
        e.Hit(damage + UpgradeManager.instance.blueDiceUpgrade);
    }
}
