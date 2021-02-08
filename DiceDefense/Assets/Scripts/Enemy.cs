using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _hp;

    public void Hit(int dmg)
    {
        _hp -= dmg;

        if(_hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {

    }
}
