using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    { 
        livesText.text = "Lives: " + playerController.lives;
        scoreText.text = "Score: " + playerController.currentScore;
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
    }
}
