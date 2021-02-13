using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    private const float MoveSpeed = 0.05f;

    public Enemy targetEnemy;
    
    private Action<Enemy> _act;

    private SpriteRenderer _sr;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(targetEnemy != null && targetEnemy.gameObject.activeSelf)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetEnemy.transform.position, MoveSpeed);
        }
        else
        {
            _act = null;
            ObjectPoolManager.instance.ReturnBullet(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == targetEnemy.gameObject)
        {
            if(_act != null)
            {
                _act.Invoke(targetEnemy);
                _act = null;
            }

            ObjectPoolManager.instance.ReturnBullet(this);
        }
    }

    public void AddAction(Action<Enemy> act)
    {
        _act += act;
    }

    public void SetBulletColor(Color c)
    {
        _sr.color = c;
    }

}
