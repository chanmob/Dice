﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGameMainUI : MonoBehaviour
{
    public void PurchaseDice()
    {
        int cost = InGameManager.instance.Count * 10 + 10;

        if (InGameManager.instance.Money >= 0)
        {
            InGameManager.instance.GetMoney(-cost);
            InGameManager.instance.CreateDice();
        }
    }
}
