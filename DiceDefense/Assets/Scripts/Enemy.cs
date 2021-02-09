using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _hp;

    private float speed = 1f;

    private bool isDie = false;

    public void Hit(int dmg)
    {
        _hp -= dmg;

        if(_hp <= 0)
        {
            Die();
        }
    }

    public void Slow(float amount)
    {

    }

    public void Paralysis(float time)
    {

    }

    private void Die()
    {
        if (isDie == false)
            isDie = true;


    }

    private IEnumerator MoveCoroutine()
    {
        while (true)
        {
            yield return null;
        }
    }
}
