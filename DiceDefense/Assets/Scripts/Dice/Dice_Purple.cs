using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Purple : Dice
{
    private const int CriticalDmg = 3;
    private const int CriticalDmgPerLv = 1;

    public override void Attack(Enemy e, Bullet b)
    {
        bool cri = false;

        int randomCritical = Random.Range(1, 101);
        if (randomCritical <= critical)
            cri = true;

        if (cri)
        {
            e.Hit((damage + UpgradeManager.instance.purpleDiceUpgrade) * 2);

            ParticleDisable p = ObjectPoolManager.instance.GetHitParticle(true);
            p.transform.position = b.transform.position;
            p.gameObject.SetActive(true);
        }
        else
        {
            e.Hit(damage + UpgradeManager.instance.purpleDiceUpgrade);

            ParticleDisable p = ObjectPoolManager.instance.GetHitParticle(false);
            p.transform.position = b.transform.position;
            p.gameObject.SetActive(true);
        }
    }

    public override void SkillAttack(Enemy e, Bullet b)
    {
        e.Hit((damage + UpgradeManager.instance.purpleDiceUpgrade) * CriticalDmg);
    }
}
