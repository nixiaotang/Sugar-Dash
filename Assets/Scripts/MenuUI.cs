using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField]
    private Transform playButton;

    [SerializeField]
    private Transform quitButton;

    public void PlayButton() {
        SceneManager.LoadScene(1);
    }

    public void QuitButton() {
        Application.Quit();
    }

    public void OnHoverPlay() {
        playButton.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2 (110, 110);
    }

    public void OffHoverPlay() {
        playButton.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2 (100, 100);
    }

    public void OnHoverQuit() {
        quitButton.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2 (110, 110);
    }

    public void OffHoverQuit() {
        quitButton.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2 (100, 100);
    }

}
