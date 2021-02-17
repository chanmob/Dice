using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEffect : MonoBehaviour
{
    private const float disableTime = 0.5f;

    private IEnumerator _coroutine;

    private void OnEnable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        _coroutine = CircleDisableCoroutine();
        StartCoroutine(_coroutine);
    }

    private IEnumerator CircleDisableCoroutine()
    {
        yield return new WaitForSeconds(disableTime);

        ObjectPoolManager.instance.ReturnCircle(this);
    }
}
