using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    //1당 0.45
    public float a;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, a);
    }
}
