using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const float minDistance = 0.1f;

    private int _hp;

    private float speed = 0.0075f;

    private bool isDie = false;

    private IEnumerator _coroutine;

    private void OnEnable()
    {
        isDie = false;

        _coroutine = MoveCoroutine();
        StartCoroutine(_coroutine);
    }

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
        if (isDie)
            return;

        isDie = true;

        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        InGameManager.instance.roundEnemy.Remove(this);
        ObjectPoolManager.instance.ReturnEnemy(this);
    }

    private IEnumerator MoveCoroutine()
    {
        int idx = 0;
        int lineLen = InGameManager.instance.lines.Length;

        while (idx < lineLen)
        {
            transform.position = Vector2.MoveTowards(transform.position, InGameManager.instance.lines[idx].position, speed);

            float diff = Vector2.Distance(transform.position, InGameManager.instance.lines[idx].position);

            if (diff <= minDistance)
                idx++;

            yield return null;
        }
    }
}
