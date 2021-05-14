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
        StartCoroutine(InitCoin());
    }

    //宝箱动画
    private void SwitchAnimation()
    {
        chestAnim.SetTrigger("box_close_1");
    }
    
    //生成金币
    private IEnumerator InitCoin()
    {
        int initCoinNum = itemID * 5;
        while (initCoinNum>0)
        {
            GameObject coin = ObjectsPool.instance.GetInstance();
            coin.transform.position = initCoinTransform.position+new Vector3(UnityEngine.Random.Range(-0.3f,0.3f),
                UnityEngine.Random.Range(-0.3f,0.3f),0);
            Vector3 coinLocalScale = coin.transform.localScale;
            coin.transform.localScale = new Vector3(0, 0, 1);
            Tweener coinTweener = coin.transform.DOMove(coinTransform.position, 1);
            coin.transform.DOScale(coinLocalScale, 0.2f);
            coinTweener.SetEase(Ease.InExpo);
            
            StartCoroutine(TweenerPlayerComplete(coin));
            initCoinNum--;
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(0.5f);
        txtPurchaseTips.gameObject.SetActive(false);
    }

    //金币回归对象池
    private IEnumerator TweenerPlayerComplete(GameObject coin)
    {
        yield return new WaitForSeconds(1);
        ObjectsPool.instance.ReturnInstance(coin);
        txtCoinNum.text = (int.Parse(txtCoinNum.text) + 1).ToString();
    }
}
