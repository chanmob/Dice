using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataManager : Singleton<ItemDataManager>
{
    private Dictionary<string, ItemData> itemDatas;

    protected override void OnAwake()
    {
        base.OnAwake();

        itemDatas = new Dictionary<string, ItemData>();
    }
}
