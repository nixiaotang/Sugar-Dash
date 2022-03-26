using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterManager : MonoBehaviour
{

    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private Player player;

    public EmptyCounter[] emptyCounters = new EmptyCounter[6];
    private int[,] emptyCounterPos = new int[6, 2] {{0, 3}, {0, 5}, {0, 6}, {1, 8}, {5, 5}, {4, 0}};
    private int[] trashCanPos = new int[2] {0, 7};
    public CuttingBoard[] cuttingBoards = new CuttingBoard[2];
    private int[,] cuttingBoardPos = new int [2, 2] {{5, 3}, {5, 4}};
    private int[] chocolateCounterPos = new int[2] {5, 2};
    private int[] strawberryCounterPos = new int[2] {5, 1};
    private int[] doughCounterPos = new int[2] {1, 0};
    private int[] eggCounterPos = new int[2] {2, 0};
    private int[] cupCounterPos = new int[2] {4, 8};
    private int[,] submitCounterPos = new int[2, 2] {{5, 6}, {5, 7}};
    private int[,] validOrders = new int[4, 2] {{0, 2}, {0, 4}, {1, 2}, {1, 5}};

    public Mixer[] mixers = new Mixer[2];
    private int[,] mixerPos = new int[2, 2] {{2, 8}, {3, 8}};
    public Oven[] ovens = new Oven[2];
    private int[,] ovenPos = new int[2, 2] {{0, 1}, {0, 2}};


    public int[] take(int r, int c) {
        //empty counter
        for(int i = 0; i < emptyCounters.Length; i++) {
            if(r == emptyCounterPos[i, 0] && c == emptyCounterPos[i, 1]) {
                return emptyCounters[i].takeItem();
            }
        }

        //cutting board
        for(int i = 0; i < cuttingBoards.Length; i++) {
            if(r == cuttingBoardPos[i,0] && c == cuttingBoardPos[i,1]) {
                return cuttingBoards[i].takeItem();
            }
        }

        //chocolate counter
        if(r == chocolateCounterPos[0] && c == chocolateCounterPos[1]) return new int[2] {0, 0};

        //chocolate counter
        if(r == strawberryCounterPos[0] && c == strawberryCounterPos[1]) return new int[2] {1, 0};
        
        //dought counter
        if(r == doughCounterPos[0] && c == doughCounterPos[1]) return new int[2] {2, 0};

        //egg counter
        if(r == eggCounterPos[0] && c == eggCounterPos[1]) return new int[2] {2, 1};

        //cup counter
        if(r == cupCounterPos[0] && c == cupCounterPos[1]) return new int[2] {1, 4};

        //mixer
        for(int i = 0; i < mixers.Length; i++) {
            if(r == mixerPos[i, 0] && c == mixerPos[i, 1]) return mixers[i].pickUp();
        }

        //oven
        for(int i = 0; i < ovens.Length; i++) {
            if(r == ovenPos[i, 0] && c == ovenPos[i, 1]) return ovens[i].takeOut();
        }

        return new int[2] {-1, -1};
    }

    public bool putDown(int r, int c, int Ir, int Ic) {
        
        //empty counter
        for(int i = 0; i < emptyCounters.Length; i++) {
            if(r == emptyCounterPos[i, 0] && c == emptyCounterPos[i, 1]) {
                return emptyCounters[i].putDownItem(Ir, Ic);
            }
        }

        //trash can
        if(r == trashCanPos[0] && c == trashCanPos[1]) {   
            return true; //if trash can, throw away item regardless of what it is
        }

        //oven
        for(int i = 0; i < ovens.Length; i++) {

            if(r == ovenPos[i, 0] && c == ovenPos[i, 1] && Ir == 2 && Ic == 3) {
                int type = player.GetCarryItem()[2];

                if(type == 3) return false;
                else {
                    return ovens[i].putIn(type);
                }
            }
        }

        if(Ir == 2 && Ic == 3) return false;

        //cutting board
        for(int i = 0; i < cuttingBoards.Length; i++) {
            if(r == cuttingBoardPos[i,0] && c == cuttingBoardPos[i,1]) {
                return cuttingBoards[i].putDownItem(Ir, Ic);
            }
        }

        //mixer
        for(int i = 0; i < mixers.Length; i++) {
            if(r == mixerPos[i, 0] && c == mixerPos[i, 1]) {
                return mixers[i].putMaterial(Ir, Ic);
            }
        }

        //submit counter
        for(int i = 0; i < 2; i++) {
            if(r == submitCounterPos[i, 0] && c == submitCounterPos[i, 1]) {
                for(int j = 0; j < 4; j++) {
                    if(Ir == validOrders[j, 0] && Ic == validOrders[j, 1]) {
                        gameManager.submitOrder(j);
                        return true;
                    }
                }
            }
        }
        

        return false;
    }


}
