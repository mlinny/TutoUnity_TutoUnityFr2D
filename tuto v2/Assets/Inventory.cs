using UnityEngine.UI;
using UnityEngine;

public class Inventory : MonoBehaviour
{


    #region   Coint 
    public Text coinText;
    public int cointCount;

    public void AddCoin()
    {
        AddCoin(1);
    }
    public void AddCoin(int _coin)
    {
        cointCount += _coin;
        coinText.text = cointCount.ToString();

    }


#endregion

    public static Inventory instance;

    private void Awake()
    {
        if (instance = null)
        {
            Debug.LogWarning("Il y a plus d'une instance dans la scène");
            return;
        }
        instance = this;

    }



}
