using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreDialog : MonoBehaviour
{
    [SerializeField] private ChestItem chestItem;
    [SerializeField] private Transform content;
    [SerializeField] private Transform coinTransform;
    [SerializeField] private Transform initCoinTransform;
    [SerializeField] private Text txtCoinNum;

    private List<ChestItem> chestItemList = new List<ChestItem>();
    
    //初始化商店列表
    public void Init()
    {
        for (int i = 0; i < 3; i++)
        {
            ChestItem item = Instantiate(chestItem, content);
            item.InitItems(coinTransform, initCoinTransform, txtCoinNum, i + 1);

            chestItemList.Add(item);
        }
    }
}
