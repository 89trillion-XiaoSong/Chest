using UnityEngine;

public class HomePage : MonoBehaviour
{
    [SerializeField] private StoreDialog storeDialog;

    //打开商店
    public void OpenStore()
    {
        StoreDialog store = Instantiate(storeDialog, transform);
        store.Init();
    }
}
