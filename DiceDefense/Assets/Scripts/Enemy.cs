using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const float minDistance = 0.1f;

    private int _hp;

    private float speed = 0.0075f;
    private float slowdown = 0;
    private float lastSlowdownTime;

    private bool blockParalysis = false;
    private bool isDie = false;
    public bool isSlowDown = false;
    public bool isParalysis = false;

    private IEnumerator _coroutine;
    private IEnumerator _paralysisCoroutine;

    private void OnEnable()
    {
        isDie = false;
        slowdown = 0;

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

    public void Slow(float amount, float time)
    {
        if(slowdown < amount)
            slowdown = amount;

        isSlowDown = true;
        lastSlowdownTime = Time.time + time;
    }

    public void Paralysis(float time)
    {
        if (blockParalysis)
            return;

        blockParalysis = true;
        isParalysis = true;

        if(_paralysisCoroutine != null)
        {
            StopCoroutine(_paralysisCoroutine);
            _paralysisCoroutine = null;
        }

        _paralysisCoroutine = ParalysisCoroutine(time);
        StartCoroutine(_paralysisCoroutine);
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

        InGameManager.instance.GetMoney(10);
        InGameManager.instance.roundEnemy.Remove(this);
        ObjectPoolManager.instance.ReturnEnemy(this);
    }

    private IEnumerator MoveCoroutine()
    {
        int idx = 0;
        int lineLen = InGameManager.instance.lines.Length;

        while (idx < lineLen)
        {
            if (isSlowDown && Time.time >= lastSlowdownTime)
            {
                isSlowDown = false;
                slowdown = 0;
            }

            yield return new WaitUntil(() => isParalysis == false);

            transform.position = Vector2.MoveTowards(transform.position, InGameManager.instance.lines[idx].position, speed * (1 - slowdown));

            float diff = Vector2.Distance(transform.position, InGameManager.instance.lines[idx].position);

            if (diff <= minDistance)
                idx++;

            yield return null;
        }
    }

    private IEnumerator ParalysisCoroutine(float time)
    {
        isParalysis = true;

        yield return new WaitForSeconds(time);

        isParalysis = false;

        yield return new WaitForSeconds(0.5f);

        blockParalysis = false;
        _paralysisCoroutine = null;
    }
}
