using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public GameObject gameCanvas;
    public GameObject deathCanvas;

    [SerializeField] private TMP_Text scoreUI;

    private void Start()
    {
        gameCanvas.SetActive(true);
        deathCanvas.SetActive(false);
        GameManager.GameCursor();
        Time.timeScale = 1f;
    }

    public void DeathMenu(int score)
    {
        Time.timeScale = 0f;
        GameManager.MenuCursor();
        scoreUI.text = score.ToString();
        gameCanvas.SetActive(false);
        deathCanvas.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
