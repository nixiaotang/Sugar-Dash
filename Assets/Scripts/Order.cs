using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{

    public GameObject[] items = new GameObject[4];

    private int order;


    public void StartOrder(int item) {
        items[item].SetActive(true);
        order = item;
    }
}
