using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour
{
    [SerializeField]
    private GameObject cookie, chocolateCupcake, strawberryCupcake;
    [SerializeField]
    private GameObject progressBar;

    private int[,] itemIDs = new int[3,2] {{0, 2}, {0, 4}, {1, 2}};

    private int state; //-1 = empty, 0 = cooking, 1 = cookie, 2 = chocoCupcake, 3 = strawberryCupcake
    private float progress;
    private int curType;

    void Start() {
        empty();
        progress = 1f;
        progressBar.transform.localScale = new Vector3(0f, 0.1f, 0.1f);
        progressBar.SetActive(false);
    }

    void Update() {

        if(state == 0) {
            progress -= 0.0005f;
            progressBar.gameObject.transform.localScale = new Vector3(progress, 0.1f, 0.1f);

            if(progress <= 0f) {
                progress = 1f;
                progressBar.SetActive(false);
                finishedBaking();
            }
        }
    }

    private void empty() {
        state = -1;
        cookie.SetActive(false);
        chocolateCupcake.SetActive(false);
        strawberryCupcake.SetActive(false);
    }

    public bool putIn(int type) {
        if(state == -1) {
            curType = type;
            state = 0;
            progressBar.SetActive(true);
            return true;
        }

        return false;
    }

    private void finishedBaking() {
        if(curType == 0) cookie.SetActive(true);
        else if(curType == 1) chocolateCupcake.SetActive(true);
        else if (curType == 2) strawberryCupcake.SetActive(true);

        state = curType+1;
    }

    public int[] takeOut() {
        if(state <= 0) return new int[2] {-1, -1};
        else {

            int result = state-1;

            empty();
            return new int[2] {itemIDs[result, 0], itemIDs[result, 1]};

        }

    }
    
}
