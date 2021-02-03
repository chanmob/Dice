using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    [SerializeField]
    private Dice _dice_Red_Prefab;
    [SerializeField]
    private Dice _dice_Blue_Prefab;
    [SerializeField]
    private Dice _dice_Green_Prefab;
    [SerializeField]
    private Dice _dice_Purple_Prefab;
    [SerializeField]
    private Dice _dice_Yellow_Prefab;

    private Stack<Dice> _stack_RedDice;
    private Stack<Dice> _stack_BlueDice;
    private Stack<Dice> _stack_GreenDice;
    private Stack<Dice> _stack_PurpleDice;
    private Stack<Dice> _stack_YellowDice;

    [SerializeField]
    private Enemy _enemy_Prefab;

    private Stack<Enemy> _stack_Enemy;

    protected override void OnAwake()
    {
        base.OnAwake();

        _stack_RedDice = new Stack<Dice>();
        _stack_BlueDice = new Stack<Dice>();
        _stack_GreenDice = new Stack<Dice>();
        _stack_PurpleDice = new Stack<Dice>();
        _stack_YellowDice = new Stack<Dice>();

        _stack_Enemy = new Stack<Enemy>();
    }

    public Dice GetDice(DiceType diceType)
    {
        Dice dice = null;

        switch (diceType)
        {
            case DiceType.Red:
                if (_stack_RedDice.Count == 0)
                {
                    MakeDice(DiceType.Red, 1);
                }
                dice = _stack_RedDice.Pop();
                break;
            case DiceType.Blue:
                if (_stack_BlueDice.Count == 0)
                {
                    MakeDice(DiceType.Blue, 1);
                }
                dice = _stack_BlueDice.Pop();
                break;
            case DiceType.Green:
                if (_stack_GreenDice.Count == 0)
                {
                    MakeDice(DiceType.Green, 1);
                }
                dice = _stack_GreenDice.Pop();
                break;
            case DiceType.Purple:
                if (_stack_PurpleDice.Count == 0)
                {
                    MakeDice(DiceType.Purple, 1);
                }
                dice = _stack_PurpleDice.Pop();
                break;
            case DiceType.Yellow:
                if (_stack_YellowDice.Count == 0)
                {
                    MakeDice(DiceType.Yellow, 1);
                }
                dice = _stack_YellowDice.Pop();
                break;
        }

        return dice;
    }

    public void ReturnDice(DiceType diceType, Dice dice)
    {
        switch (diceType)
        {
            case DiceType.Red:
                _stack_RedDice.Push(dice);
                break;
            case DiceType.Blue:
                _stack_BlueDice.Push(dice);
                break;
            case DiceType.Green:
                _stack_GreenDice.Push(dice);
                break;
            case DiceType.Purple:
                _stack_PurpleDice.Push(dice);
                break;
            case DiceType.Yellow:
                _stack_YellowDice.Push(dice);
                break;
        }

        if (dice.gameObject.activeSelf)
            dice.gameObject.SetActive(false);
    }

    private void MakeDice(DiceType diceType, int count)
    {
        Dice dicePrefab = null;

        switch (diceType)
        {
            case DiceType.Red:
                dicePrefab = _dice_Red_Prefab;
                break;
            case DiceType.Blue:
                dicePrefab = _dice_Blue_Prefab;
                break;
            case DiceType.Green:
                dicePrefab = _dice_Green_Prefab;
                break;
            case DiceType.Purple:
                dicePrefab = _dice_Purple_Prefab;
                break;
            case DiceType.Yellow:
                dicePrefab = _dice_Yellow_Prefab;
                break;
        }

        for (int i = 0; i < count; i++)
        {
            Dice newDice = Instantiate(dicePrefab);
            
            switch (diceType)
            {
                case DiceType.Red:
                    _stack_RedDice.Push(newDice);
                    break;
                case DiceType.Blue:
                    _stack_BlueDice.Push(newDice);
                    break;
                case DiceType.Green:
                    _stack_GreenDice.Push(newDice);
                    break;
                case DiceType.Purple:
                    _stack_PurpleDice.Push(newDice);
                    break;
                case DiceType.Yellow:
                    _stack_YellowDice.Push(newDice);
                    break;
            }

            newDice.gameObject.SetActive(false);
        }
    }

    public Enemy GetEnemy()
    {
        Enemy enemy = null;

        return enemy;
    }

    public void ReturnEnemy()
    {

    }

    private void MakeEnemy(int count)
    {

    }
}
