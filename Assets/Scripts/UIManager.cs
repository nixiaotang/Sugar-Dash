using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI coinText;
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private TextMeshProUGUI gameOverCoinText;


    [SerializeField]
    private GameObject order;
    [SerializeField]
    private Transform orderParent;
    
    public void UpdateCoinText(int coins) { 
        coinText.text = coins.ToString();
        gameOverCoinText.text = coins.ToString();
    }

    public void UpdateTimeText(int time) { 

        string seconds = (time%60).ToString();
        if(seconds.Length == 1) seconds = "0" + seconds;

        timeText.text = (Mathf.Floor(time/60)).ToString() + ":" + seconds;
    }

    public void NewOrder(int item, int number) {

        GameObject orderObj = Instantiate(order, this.transform.position, this.transform.rotation, orderParent);
        orderObj.transform.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-300f + (float)number * 75f, 140f, 0f);
        orderObj.transform.gameObject.GetComponent<Order>().StartOrder(item);
    }

    public void UpdateOrders(ArrayList orders) {

        foreach (Transform child in orderParent) GameObject.Destroy(child.gameObject);

        for(int i = 0; i < orders.Count; i++) {

            GameObject orderObj = Instantiate(order, this.transform.position, this.transform.rotation, orderParent);
            orderObj.transform.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-300f + (float)i * 75f, 140f, 0f);
            orderObj.transform.gameObject.GetComponent<Order>().StartOrder((int)orders[i]);

        }
    }


}
