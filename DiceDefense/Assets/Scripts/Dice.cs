using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DiceType
{
    Red,
    Blue,
    Green,
    Purple,
    Yellow,
    None
}

public class Dice : MonoBehaviour
{
    public DiceType diceType = DiceType.None;

    public int damage;
    public int mp;
    public int perMP;
    public int critical;
    public int addDamage;
    public int addMP;
    public int addPerMP;
    public int addCritical;

    private int lv;

    public float attackSpeed;
    public float addAttackSpeed;
}