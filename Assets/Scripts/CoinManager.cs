using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int totalCoins = 3;
    private int coinsCollected = 0;
    private bool doorOpened = false;

    public void CollectCoin()
    {
        coinsCollected++;
        Debug.Log("Coins: " + coinsCollected + "/" + totalCoins);

        if (coinsCollected >= totalCoins && !doorOpened)
        {
            doorOpened = true;
            GetComponent<Door>()?.Open();
        }
    }
}