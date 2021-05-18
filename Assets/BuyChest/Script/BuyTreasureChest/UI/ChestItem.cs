using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChestItem : MonoBehaviour
{
    private Transform coinTransform;
    private Transform initCoinTransform;
    private Text txtCoinNum;
    private Text txtPurchaseTips;
    
    [SerializeField] private Text txtCostGold;
    [SerializeField] private Animator chestAnim;
    
    private int itemID;
    private const int CostGold = 250;

    /// <summary>
    /// 生成商品
    /// </summary>
    /// <param name="coinTransform">金币动画终点</param>
    /// <param name="initCoinTransform">金币生成位置</param>
    /// <param name="itemID">商品ID</param>
    /// <param name="txtCoinNum">金币数量</param>>
    /// <param name="txtPurchaseTips">购买提示</param>>
    public void InitItems(Transform coinTransform,Transform initCoinTransform,Text txtCoinNum,Text txtPurchaseTips,int itemID)
    {
        this.coinTransform = coinTransform;
        this.initCoinTransform = initCoinTransform;
        this.itemID = itemID;
        this.txtCoinNum = txtCoinNum;
        this.txtPurchaseTips = txtPurchaseTips;
        
        ChestCost();
    }

    //金币花费
    private void ChestCost()
    {
        txtCostGold.text = (itemID * CostGold).ToString();
    }
    
    //购买操作
    public void PurchaseChest()
    {
        txtPurchaseTips.gameObject.SetActive(true);
        
        SwitchAnimation();
        InitCoin();
        //StartCoroutine(InitCoin());
    }

    //宝箱动画
    private void SwitchAnimation()
    {
        chestAnim.SetTrigger("box_close_1");
    }


    //生成金币
    private void InitCoin()
    {
        int initCoinNum = itemID * 5;
        Sequence sequence = DOTween.Sequence();
        
        for(int i=0;i<initCoinNum;i++)
        {
            GameObject coin = ObjectsPool.instance.GetInstance();
            coin.SetActive(false);
            coin.transform.position = initCoinTransform.position+new Vector3(UnityEngine.Random.Range(-0.3f,0.3f),
                UnityEngine.Random.Range(-0.3f,0.3f),0);
            Vector3 coinLocalScale = coin.transform.localScale;
            coin.transform.localScale = new Vector3(0, 0, 1);

            sequence.InsertCallback(i * 0.2f, () =>
            {
                coin.SetActive(true);
            });
            sequence.Insert(i * 0.2f, coin.transform.DOScale(coinLocalScale, 0.2f));
            
            //金币移动动画，并在动画完成调用回调函数，将金币放入对象池
            Tweener coinTweener = coin.transform.DOMove(coinTransform.position, 1).OnComplete(() =>
            {
                ObjectsPool.instance.ReturnInstance(coin);
                txtCoinNum.text = (int.Parse(txtCoinNum.text) + 1).ToString();
            });;
            coinTweener.SetEase(Ease.InExpo);
            sequence.Insert(i * 0.2f, coinTweener);
        }
    }
    
}
