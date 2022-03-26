using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : MonoBehaviour
{

    [SerializeField]
    private Items items;
    [SerializeField]
    private Transform itemParent;

    private int[] curItem;
    private int[] chocolateItem = {0, 0};
    private int[] strawberyItem = {1, 0};

    void Start() {
        curItem = new int[2] {-1, -1};
        
    }

    public int[] takeItem() {

        foreach (Transform child in itemParent) GameObject.Destroy(child.gameObject);

        int[] prevItem = curItem;
        curItem = new int[2] {-1, -1}; //clear counter

        return prevItem; //if returned [-1, -1] then FAILEDDDDDD

    }

    public bool putDownItem(int Ir, int Ic) {

        if(curItem[0] == -1 && curItem[1] == -1) { //if cutting board empty
            
            if(Ir == chocolateItem[0] && Ic == chocolateItem[1]) { //chocolate -> cut chocolate
                curItem = new int[2] {0, 1};
                GameObject item  = Instantiate(items.getItem(0, 1), this.transform.position, this.transform.rotation, itemParent);
                return true;

            } else if(Ir == strawberyItem[0] && Ic == strawberyItem[1]) { //strawberry -> cut strawberry
                curItem = new int[2] {1, 1};
                GameObject item  = Instantiate(items.getItem(1, 1), this.transform.position, this.transform.rotation, itemParent);
                return true;

            } else { //non-cuttable objects, just drop on cutting board
                curItem = new int[2] {Ir, Ic};
                GameObject item  = Instantiate(items.getItem(Ir, Ic), this.transform.position, this.transform.rotation, itemParent);
                return true;
            }

        } else return false; //cutting board full

    }

}
