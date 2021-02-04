using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : Singleton<InGameManager>
{
    private List<Enemy> _roundEnemy;

    private int _round;

    private void Start()
    {
        _roundEnemy = new List<Enemy>();
    }

    public Enemy GetNearEnemy(Vector3 dicePos)
    {
        int cnt = _roundEnemy.Count;

        if (cnt == 0)
            return null;

        Enemy enemy = null;
        float distance = float.MaxValue;

        for(int i = 0; i < cnt; i++)
        {
            float diff = (_roundEnemy[i].transform.position - dicePos).sqrMagnitude;

            if(diff < distance)
            {
                distance = diff;
                enemy = _roundEnemy[i];
            }
        }

        return enemy;
    }
}