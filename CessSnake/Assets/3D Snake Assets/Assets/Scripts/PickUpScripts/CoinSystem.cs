using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinSystem : MonoBehaviour
{

    public Text coinDisplayText;
    public Text coinDisplayText1;
    public int currentCoins = 0;


    // Start is called before the first frame update
    private void Start()
    {
        if (PlayerPrefs.HasKey("Coin"))
        {
            currentCoins = PlayerPrefs.GetInt("Coin");
        }

        coinDisplayText.text = "Coins: " + currentCoins;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider target)
    {

        if (target.tag == Tags.FRUIT)
        {

            currentCoins += 1;
            PlayerPrefs.SetInt("Coin", currentCoins);
            coinDisplayText.text = "Coins:" + currentCoins;
            coinDisplayText1.text = "Coins:" + currentCoins;
        }


    }
}
