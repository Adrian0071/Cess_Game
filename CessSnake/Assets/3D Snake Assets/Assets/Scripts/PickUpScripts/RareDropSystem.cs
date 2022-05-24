using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RareDropSystem : MonoBehaviour
{

    public Text SnakePartDisplayText;
    public Text SnakePartDisplayText1;
    public int snakePartCount = 0;


    // Start is called before the first frame update
    private void Start()
    {
        if (PlayerPrefs.HasKey("Snake Parts"))
        {
            snakePartCount = PlayerPrefs.GetInt("Snake Parts");
        }

       SnakePartDisplayText.text = "Snake Parts: " + snakePartCount;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider target)
    {

        if (target.tag == Tags.DROP)
        {

           snakePartCount += 1;
            PlayerPrefs.SetInt("Snake Parts:", snakePartCount);
            SnakePartDisplayText.text = "Snake Parts:" + snakePartCount;
           SnakePartDisplayText1.text = "Snake Parts:" + snakePartCount;
        }


    }
}
