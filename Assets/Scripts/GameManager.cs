using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private GameObject gameOverScreen;

    [SerializeField]
    private Transform playButton;
    [SerializeField]
    private Transform menuButton;

    private AudioSource timerSound;
    private bool timerSoundStarted;


    private ArrayList activeOrders = new ArrayList();
    private int[] costs = new int[4] {10, 20, 20, 5};

    private int coins;
    private int time;

    private float timer = 0f;
    private float oldTimer = 0f;

    void Start() {
        Time.timeScale = 1;

        coins = 0;
        time = 60;

        timerSoundStarted = false;
        timerSound = GetComponent<AudioSource>();

        uiManager.UpdateCoinText(coins);
        uiManager.UpdateTimeText(time);

        createOrder();
        createOrder();

        gameOverScreen.SetActive(false);

    }

    void Update() {
        timer = (timer + Time.deltaTime);
        uiManager.UpdateTimeText(time - (int)timer);

        if(timer - oldTimer > 7f && activeOrders.Count < 6) {
            createOrder();
            oldTimer = timer;
        }

        if(timer >= 60) {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }

        if(timer >= 50 && !timerSoundStarted) {
            timerSound.Play();
            timerSoundStarted = true;
        }

        if(Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }


    public void MenuButton() {
        SceneManager.LoadScene(0);
    }

    public void PlayButton() {
        SceneManager.LoadScene(1);
    }

    public void createOrder() {
        int item = Random.Range(0, 4);
        uiManager.NewOrder(item, activeOrders.Count);
        activeOrders.Add(item);
    }

    public void submitOrder(int order) {
        if(activeOrders.IndexOf(order) != -1) {
            activeOrders.Remove(order);
            coins += costs[order];

            uiManager.UpdateOrders(activeOrders);
            uiManager.UpdateCoinText(coins);
        }
    }


    public void OnHoverPlay() {
        playButton.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2 (110, 110);
    }

    public void OffHoverPlay() {
        playButton.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2 (100, 100);
    }

    public void OnHoverMenu() {
        menuButton.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2 (110, 110);
    }

    public void OffHoverMenu() {
        menuButton.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2 (100, 100);
    }

}
