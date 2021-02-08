using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : Singleton<InGameManager>
{
    public readonly float InfoDistance = 0.3f;
    public readonly float CollaboDistance = 0.5f;

    public readonly string[] DiceColorHexCode = new string[5]
    {
        "#FFACB5","#9BDAF2","#C7E8A7","#9F8DE8","#FFDF9E"
    };

    private List<Enemy> _roundEnemy;

    public List<Dice> _createdDice;

    private int _round = 0;
    private int _money;

    private void Start()
    {
        _roundEnemy = new List<Enemy>();
        _createdDice = new List<Dice>();
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

    private IEnumerator RoundCoroutine()
    {
        while (true)
        {
            _round++;


            yield return new WaitUntil(() => _roundEnemy.Count == 0);
        }
    }

    public void GetMoney(int money)
    {
        _money += money;
    }
}