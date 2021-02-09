using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : Singleton<InGameManager>
{
    private const float GenerationCycleTime = 0.5f;

    public readonly float InfoDistance = 0.3f;
    public readonly float CollaboDistance = 0.5f;

    public readonly string[] DiceColorHexCode = new string[5]
    {
        "#FFACB5","#9BDAF2","#C7E8A7","#9F8DE8","#FFDF9E"
    };

    public List<Enemy> roundEnemy;

    public List<Dice> createdDice;

    public List<Vector2> createPositionList;

    [SerializeField]
    private GameObject[] lines;

    [SerializeField]
    private Transform _createPositionParent;

    private int _round = 0;
    private int _money;
    private int _count;

    public int Count {
        get { return _count; }
    }

    public int Money {
        get {
            return _money;
        }
    }

    private void Start()
    {
        roundEnemy = new List<Enemy>();
        createdDice = new List<Dice>();
        createPositionList = new List<Vector2>();

        Transform[] positions = _createPositionParent.GetComponentsInChildren<Transform>();
        int len = positions.Length;
        for (int i = 1; i < len; i++)
        {
            createPositionList.Add(positions[i].position);
        }

        StartCoroutine(RoundCoroutine());
    }

    public Enemy GetNearEnemy(Vector3 dicePos)
    {
        int cnt = roundEnemy.Count;

        if (cnt == 0)
            return null;

        Enemy enemy = null;
        float distance = float.MaxValue;

        for(int i = 0; i < cnt; i++)
        {
            float diff = (roundEnemy[i].transform.position - dicePos).sqrMagnitude;

            if(diff < distance)
            {
                distance = diff;
                enemy = roundEnemy[i];
            }
        }

        return enemy;
    }

    private IEnumerator RoundCoroutine()
    {
        while (true)
        {
            _round++;

            for(int i = 0; i < _round; i++)
            {
                Enemy e = ObjectPoolManager.instance.GetEnemy();
                roundEnemy.Add(e);
            }

            for(int i = 0; i < _round; i++)
            {
                roundEnemy[i].transform.position = lines[0].transform.position;
                roundEnemy[i].gameObject.SetActive(true);
                yield return new WaitForSeconds(GenerationCycleTime);   
            }

            yield return new WaitUntil(() => roundEnemy.Count == 0);
        }
    }

    public void GetMoney(int money)
    {
        _money += money;
    }

    public Vector2? GetCreatePoisition()
    {
        int cnt = createPositionList.Count;

        if (cnt == 0)
            return null;

        _count++;
        int randomIdx = Random.Range(0, cnt);
        return createPositionList[randomIdx];
    }

    public void CreateDice()
    {
        int cnt = createPositionList.Count;

        if (cnt == 0)
            return;

        _count++;
    
        int randomIdx = Random.Range(0, cnt);
        int randomDiceType = Random.Range(0, (int)DiceType.None);

        Dice dice = ObjectPoolManager.instance.GetDice((DiceType)randomDiceType);
        dice.pos = createPositionList[randomIdx];
        dice.transform.position = createPositionList[randomIdx];
        dice.gameObject.SetActive(true);

        createdDice.Add(dice);
        createPositionList.RemoveAt(randomIdx);
    }
}