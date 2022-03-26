using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCounter : MonoBehaviour
{
    [SerializeField]
    private Items items;

    [SerializeField]
    private Player player;

    private int[] curItem;

    void Start() {
        curItem = new int[2] {-1, -1};
    }

    public int[] takeItem() {

        foreach (Transform child in transform) GameObject.Destroy(child.gameObject);

        int[] prevItem = curItem;
        curItem = new int[2] {-1, -1}; //clear counter

        return prevItem; //if returned [-1, -1] then FAILEDDDDDD
    }

    public bool putDownItem(int Ir, int Ic) {
        if(curItem[0] == -1 && curItem[1] == -1) {
            
            curItem = player.GetCarryItem();

            if(Ir == 2 && Ic == 3) Instantiate(items.getMixture(curItem[2]), this.transform.position, this.transform.rotation, this.transform);
            else Instantiate(items.getItem(Ir, Ic), this.transform.position, this.transform.rotation, this.transform);
            
            return true; //success

        } else return false; //counter not empty
    }
}
