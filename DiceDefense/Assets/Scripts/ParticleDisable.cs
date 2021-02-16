using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDisable : MonoBehaviour
{
    public bool cri;

    private ParticleSystem _ps;

    private IEnumerator _coroutine;

    private void Awake()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        _coroutine = ParticleDisableCoroutine();
        StartCoroutine(_coroutine);
    }

    private IEnumerator ParticleDisableCoroutine()
    {
        while (true && _ps != null)
        {
            yield return new WaitForSeconds(0.5f);
            if (!_ps.IsAlive(true))
            {
                _coroutine = null;
                ObjectPoolManager.instance.ReturnParticle(this);
            }
        }
    }
}
