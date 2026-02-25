using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private int coinCount = 0;

    public int GetCoinCount()
    {
        return coinCount;
    }

    public void AddCoin()
    {
        coinCount++;
    }

    public void ResetCoins()
    {
        coinCount = 0;
    }
}