using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    private Dice _dice_Red_Prefab;
    private Dice _dice_Blue_Prefab;
    private Dice _dice_Green_Prefab;
    private Dice _dice_Purple_Prefab;
    private Dice _dice_Yellow_Prefab;

    private Stack<Dice> _stack_RedDice;
    private Stack<Dice> _stack_BlueDice;
    private Stack<Dice> _stack_GreenDice;
    private Stack<Dice> _stack_PurpleDice;
    private Stack<Dice> _stack_YellowDice;

    public Dice GetDice(DiceType diceType)
    {
        Dice dice = null;

        switch (diceType)
        {
            case DiceType.Red:
                break;
            case DiceType.Blue:
                break;
            case DiceType.Green:
                break;
            case DiceType.Purple:
                break;
            case DiceType.Yellow:
                break;
        }

        return dice;
    }

    public void ReturnDice(DiceType diceType)
    {
        switch (diceType)
        {
            case DiceType.Red:
                break;
            case DiceType.Blue:
                break;
            case DiceType.Green:
                break;
            case DiceType.Purple:
                break;
            case DiceType.Yellow:
                break;
        }
    }

    private void MakeDice(DiceType diceType, int count)
    {
        switch (diceType)
        {
            case DiceType.Red:
                break;
            case DiceType.Blue:
                break;
            case DiceType.Green:
                break;
            case DiceType.Purple:
                break;
            case DiceType.Yellow:
                break;
        }

        for (int i = 0; i < count; i++)
        {

        }
    }
}
