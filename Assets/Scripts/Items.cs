using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{

    public GameObject[] a = new GameObject[6];
    public GameObject[] b = new GameObject[6];
    public GameObject[] c = new GameObject[6];
    public GameObject[] d = new GameObject[6];

    public GameObject[] mixtures = new GameObject[4]; //cookie, chocolate, strawbery, poo

    public GameObject getItem(int row, int col) {
        if(row == 0) return a[col];
        else if (row == 1) return b[col];
        else if (row == 2) return c[col];
        else if (row == 3) return d[col];
        else return null;
    }

    public GameObject getMixture(int id) {
        return mixtures[id];
    }
}
