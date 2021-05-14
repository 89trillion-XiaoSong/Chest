using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    [SerializeField] private GameObject proCoin;

    //金币对象池，设置单例模式。
    public static ObjectsPool instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private Queue<GameObject> poolInstanceQueue = new Queue<GameObject>();      //对象池列表

    /// <summary>
    /// 获得对象池对象
    /// </summary>
    /// <returns></returns>
    public GameObject GetInstance()
    {
        if (poolInstanceQueue.Count > 0)
        {
            GameObject instanceToReuse = poolInstanceQueue.Dequeue();
            instanceToReuse.SetActive(true);
            return instanceToReuse;
        }
        return Instantiate(proCoin);
    }
    
    /// <summary>
    /// 对象回归对象池
    /// </summary>
    /// <param name="gameObjectToPool"></param>
    public void ReturnInstance(GameObject gameObjectToPool)
    {
        gameObjectToPool.SetActive(false);
        poolInstanceQueue.Enqueue(gameObjectToPool);
    }
}
