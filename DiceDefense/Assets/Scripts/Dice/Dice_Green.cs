using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Green : Dice
{
    private const int MaxAttackSpeedUp = 5;

    private const float AttackSpeedIncrease = 1.1f;
    private const float AttackSpeedIncreasePerLv = 0.1f;

    private int curAttackSpeedUp = 0;

    public override void Attack(Enemy e, Bullet b)
    {
        bool cri = false;

        int randomCritical = Random.Range(1, 101);
        if (randomCritical <= critical)
            cri = true;

        if (cri)
        {
            e.Hit((damage + UpgradeManager.instance.greenDiceUpgrade) * 2);

            ParticleDisable p = ObjectPoolManager.instance.GetHitParticle(true);
            p.transform.position = e.transform.position;
            p.gameObject.SetActive(true);
        }
        else
        {
            e.Hit(damage + UpgradeManager.instance.greenDiceUpgrade);

            ParticleDisable p = ObjectPoolManager.instance.GetHitParticle(false);
            p.transform.position = e.transform.position;
            p.gameObject.SetActive(true);
        }
    }

    public override void SkillAttack(Enemy e, Bullet b)
    {
        e.Hit(damage + UpgradeManager.instance.greenDiceUpgrade);

        if (curAttackSpeedUp < MaxAttackSpeedUp)
        {
            attackSpeed *= AttackSpeedIncrease;
            curAttackSpeedUp++;
        }
    }
}
