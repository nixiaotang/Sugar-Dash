using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixer : MonoBehaviour
{
    [SerializeField]
    private Items items;
    [SerializeField]
    private GameObject bowl;

    private int[,] itemIDs = new int[4, 2] {{2, 0}, {2, 1}, {0, 1}, {1, 1}}; //dough, egg, chocolate, strawberry

    public GameObject[] materials = new GameObject[4];
    public GameObject[] mixer = new GameObject[2];
    public GameObject[] bowlMixture = new GameObject[4];

    private bool[] hasMaterials;
    private int state; //0 = empty, 1 = prepping, 2 = mixing, 3 = done, 4 = picked up
    private int type; //-1 = unknown, 0 = cookie, 1 = chocolate cupcake, 2 = strawberry cupcake, 3 = invalid

    void Start() {
        empty();
    }
    
    //reset mixer
    private void empty() {
        state = 0;
        type = -1;
        hasMaterials = new bool[4] {false, false, false, false};

        foreach (Transform child in bowl.transform) child.gameObject.SetActive(false);

        bowl.SetActive(true);
        for(int i = 0; i < 4; i++) bowlMixture[i].SetActive(false);

        mixer[0].SetActive(true);
        mixer[1].SetActive(true);
    }

    //put materials in mixer
    public bool putMaterial(int Ir, int Ic) {

        if(state != 0 && state != 1) return false; //if already mixing, can't put more materials in

        for(int i = 0; i < materials.Length; i++) {

            //check for valid materials
            if(Ir == itemIDs[i, 0] && Ic == itemIDs[i, 1]) {
                if(hasMaterials[i]) return false; //if material already in, can't put it in again
                else {
                    
                    //success
                    hasMaterials[i] = true;
                    materials[i].SetActive(true);
                    state = 1;

                    return true;

                }
            }
        }

        return false; //not a avalid material to put in

    }

    //interact with mixer
    public int[] pickUp() {

        if(state == 1) startMixer(); //start mixer if not started
        else if(state == 3) { //pick up mixer
            //state = 4;
            int[] temp = new int[3]{2, 3, type};
            empty();

            return temp;
        }
        
        return new int[2] {-1, -1};
    }

    private void startMixer() { 

        type = 3; 

        if(hasMaterials[0] && !hasMaterials[1] && hasMaterials[2] && !hasMaterials[3]) type = 0;
        else if(hasMaterials[0] && hasMaterials[1] && hasMaterials[2] && !hasMaterials[3]) type = 1;
        else if(hasMaterials[0] && hasMaterials[1] && !hasMaterials[2] && hasMaterials[3]) type = 2;

        mixer[0].SetActive(false);
        mixer[1].SetActive(false);
        bowl.SetActive(false);
        
        bowlMixture[type].SetActive(true);

        state = 3;
    }



}
