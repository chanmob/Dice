using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object Asset/ItemData", order = 2)]
public class ItemData : ScriptableObject
{
    public int addDamage;
    public int addMP;
    public int addPerMP;
    public int addCritical;

    public float addAttackSpeed;
}
