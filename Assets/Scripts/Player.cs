using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform lookAtObj;
    [SerializeField]
    private Transform carryItemParent;

    [SerializeField]
    private Items items;
    [SerializeField]
    private CounterManager counterManager;

    private Vector3 playerVelocity;
    private float playerSpeed = 2.0f;
    private string direction;

    private int row, col;
    private int lookRow, lookCol;

    private int[] carryItem = {-1, -1};
    private int[] coffeeCounterPos = new int[2] {0, 4};

    
    void Start() {

        direction = "down";

    }

     void Update() {

        if(Input.GetKeyDown(KeyCode.Space)) Interact(lookRow, lookCol);

        CalculateLook();
        Move();
    }

    void Move() {
        Vector3 move = new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero) {
            gameObject.transform.forward = move;
            animator.SetBool("isMoving", true);

            if(move.z > 0) direction = "down";
            else if (move.z < 0) direction = "up";
            else if (move.x > 0) direction = "left";
            else if (move.x < 0) direction = "right";

        } else animator.SetBool("isMoving", false);

        playerVelocity.y = 0;
        controller.Move(playerVelocity * Time.deltaTime);

    }

    void CalculateLook() {

        Vector3 pos = gameObject.transform.position;

        row = (int)Mathf.Floor((pos.z + 0.3f) / 0.6f);
        col = (int)Mathf.Floor((-pos.x + 2.7f) / 0.6f);

        lookRow = row;
        lookCol = col;

        if(direction == "up") lookRow--;
        else if (direction == "down") lookRow++;
        else if (direction == "right") lookCol++;
        else if (direction == "left") lookCol--;

        lookAtObj.position = new Vector3(-lookCol*0.6f+2.4f, 0.7f, lookRow*0.6f);
    }

    void Interact(int r, int c) {

        if(animator.GetBool("isCarrying")) { //if carrying something, try to drop it on counter

            //coffee
            if(r == coffeeCounterPos[0] && c == coffeeCounterPos[1] && carryItem[0] == 1 && carryItem[1] == 4) {
                carryItem = new int[2] {1, 5};
                foreach (Transform child in carryItemParent) GameObject.Destroy(child.gameObject);

                GameObject item = Instantiate(items.getItem(carryItem[0], carryItem[1]), this.transform.position, this.transform.rotation, carryItemParent);
                item.transform.localPosition = new Vector3(0f, 0.3f, 0.3f);
                item.transform.localScale = new Vector3(item.transform.localScale.x * 0.6f, item.transform.localScale.y * 0.6f, item.transform.localScale.z * 0.6f);
            }

            if(counterManager.putDown(r, c, carryItem[0], carryItem[1])) { //if put down successful
                carryItem = new int[2] {-1, -1};
                foreach (Transform child in carryItemParent) {
                    GameObject.Destroy(child.gameObject);
                }
                animator.SetBool("isCarrying", false);
            }

        } else { //if not carrying something, try to pick counter item up

            int[] itemTaken = counterManager.take(r, c);

            if(itemTaken[0] != -1 && itemTaken[1] != -1) {
                
                carryItem = itemTaken;
                GameObject item;

                if(carryItem.Length == 3) item = Instantiate(items.getMixture(carryItem[2]), this.transform.position, this.transform.rotation, carryItemParent);
                else item = Instantiate(items.getItem(carryItem[0], carryItem[1]), this.transform.position, this.transform.rotation, carryItemParent);
                
                item.transform.localPosition = new Vector3(0f, 0.3f, 0.3f);
                item.transform.localScale = new Vector3(item.transform.localScale.x * 0.6f, item.transform.localScale.y * 0.6f, item.transform.localScale.z * 0.6f);
                animator.SetBool("isCarrying", true);

            }

        }
    }


    public int[] GetCarryItem() {
        return carryItem;
    }



}
