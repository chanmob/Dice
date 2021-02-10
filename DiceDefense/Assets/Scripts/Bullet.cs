using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    private const float MoveSpeed = 0.05f;

    public Enemy targetEnemy;
    
    private Action<Enemy> act;

    private void Update()
    {
        if(targetEnemy != null && targetEnemy.gameObject.activeSelf)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetEnemy.transform.position, MoveSpeed);
        }
        else
        {
            act = null;
            ObjectPoolManager.instance.ReturnBullet(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == targetEnemy.gameObject)
        {
            if(act != null)
            {
                act.Invoke(targetEnemy);
                act = null;
            }

            ObjectPoolManager.instance.ReturnBullet(this);
        }
    }

    public void AddAction(Action<Enemy> a)
    {
        act += a;
    }
}
